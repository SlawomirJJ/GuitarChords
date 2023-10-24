using GuitarChords.Dtos;
using GuitarChords.Dtos.Requests;
using GuitarChords.Enums;
using GuitarChords.Models;
using GuitarChords.Repositories.Interfaces;
using GuitarChords.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GuitarChords.Repositories.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public AuthService(DataContext dbContext, IConfiguration configuration, IHttpContextAccessor contextAccessor, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;


            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public async Task<Status> Login(LoginRequest request)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user,request.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in succesfully";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User locked out";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on loggin in";
                return status;
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> Registration(RegistrationDto request)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(request.UserName);
            if (userExists !=null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }

            User user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            //role management
            if (!await _roleManager.RoleExistsAsync(request.Role))
                await _roleManager.CreateAsync(new IdentityRole(request.Role));

            if (await _roleManager.RoleExistsAsync(request.Role))
            {
                await _userManager.AddToRoleAsync(user, request.Role);
            }

            status.StatusCode = 1;
            status.Message = "User has registered succesfully";
            return status;
        }








        private Task SavingDataInCookies(string data)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Role, data)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(1),
            };

            _contextAccessor.HttpContext!.Response.Cookies.Append("token", jwt, options);
            return Task.CompletedTask;
        }
    }
}
