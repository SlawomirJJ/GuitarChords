using GuitarChords.Models.Dtos;
using GuitarChords.Models.Dtos.Requests;
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

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegistrationForm",model);
            }
            model.Role = "user";
            var result = await _authService.Registration(model);
             TempData["msg"] = result.Message;
            return RedirectToAction(nameof(RegistrationForm));
        }

        public IActionResult LoginForm()
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
                return RedirectToAction("Index", "Chord");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(LoginForm));
            }
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Index", "Chord");
        }

    }
}
