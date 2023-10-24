using GuitarChords.Dtos;
using GuitarChords.Dtos.Requests;
using GuitarChords.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.Odbc;

namespace GuitarChords.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult RegistrationForm()
        {
            return View("RegistrationForm");
        }

        public async Task<IActionResult> Registration(RegistrationDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "user";
            var result = await _authService.Registration(model);
             TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }

        public IActionResult Login()
        {
            return View("LoginForm");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginForm",request);
            }

            var result = await _authService.Login(request);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _authService.Logout();
            return View("Index");
        }

        
        //public async Task<IActionResult> Reg()
        //{
        //    var model = new RegistrationDto
        //    {
        //        UserName = "admin",
        //        Email = "max@gmail.com",
        //        Password = "Admin@12345$"
        //    };
        //    model.Role = "admin";
        //    var result = await _authService.Registration(model);
        //    return Ok(result);
        //}

    }
}
