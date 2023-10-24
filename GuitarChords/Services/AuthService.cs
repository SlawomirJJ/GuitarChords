using GuitarChords.Dtos.Requests;
using GuitarChords.Enums;
using GuitarChords.Interfaces;
using GuitarChords.Models;
using GuitarChords.Results;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GuitarChords.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(DataContext dbContext, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public string CreateAccessToken(User user)
        {
            var claims = new List<Claim>()
            {
                new("userId", user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
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

            return jwt;
        }

        public string CreateRefreshToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public bool IsTokenValid(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                }, out _);
            }
            catch (SecurityTokenValidationException)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<LoginResultDto> LoginUser(CreateUserRequest request)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new Exception("invalid_email");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new Exception("invalid_password");
            }

            string refreshToken;

            if (IsTokenValid(user.RefreshToken))
            {
                refreshToken = user.RefreshToken;
            }
            else
            {
                refreshToken = CreateRefreshToken();
                user.RefreshToken = refreshToken;
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            await SavingDataInCookies(user.Role);

            return new LoginResultDto
            {
                AccessToken = CreateAccessToken(user),
                RefreshToken = refreshToken
            };
        }

        public async Task<UseRefreshTokenResultDto> RefreshAccessToken(UseRefreshTokenRequest request)
        {
            JwtSecurityToken jwtAccessToken;
            try
            {
                jwtAccessToken = new JwtSecurityToken(request.AccessToken);
            }
            catch (Exception)
            {
                throw new Exception("invalid_token");
            }

            if (!IsTokenValid(request.RefreshToken))
            {
                throw new Exception("invalid_token");
            }

            var emailClaim = jwtAccessToken.Claims.ToList().FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email));

            if (emailClaim is null)
            {
                throw new Exception("invalid_token");
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(c =>c.Email == emailClaim.Value);

            if (user == null)
            {
                throw new Exception("invalid_email");
            }

            var claims = new List<Claim>()
            {
                new("userId", user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
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

            return new UseRefreshTokenResultDto { AccessToken = jwt };
        }

        public async Task RegisterUser(CreateUserRequest request)
        {
            if (_dbContext.Users.Any(x => x.Email == request.Email))
            {
                throw new Exception("invalid_email");
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                VerificationToken = CreateRandomToken(),
                RefreshToken = CreateRefreshToken(),
                CreatedAt = DateTime.UtcNow,
                Role = Roles.User.ToString()
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }

        public Task ResetPassword(string request)
        {
            throw new NotImplementedException();
        }

        public async Task RevokeToken(string request)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == request);

            if (result is null)
            {
                throw new Exception("invalid_token");
            }
            result.RefreshToken = "";
            _dbContext.Users.Update(result);
            await _dbContext.SaveChangesAsync();
        }

        public Task SetNewPassword(ResetPasswordRequest resetPasswordRequest, string token)
        {
            throw new NotImplementedException();
        }

        private static string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
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
