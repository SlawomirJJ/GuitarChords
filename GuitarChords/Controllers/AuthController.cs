using GuitarChords.Dtos.Requests;
using GuitarChords.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data;

namespace GuitarChords.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult ShowCreateUser()
        {
            return View("ShowCreateUser");
        }

        /// <summary>
        ///     Rejestracja użytkownika
        /// </summary>
        public async Task<ActionResult> UserRegister(CreateUserRequest request)
        {
            await _authService.RegisterUser(request);

            return View("Index");
        }

        ///// <summary>
        /////     Logowanie użytkownika - zwracanie tokenów
        ///// </summary>
        //[HttpPost("loginMobile")]
        //[ProducesResponseType(typeof(LoginResultDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult> UserLogin(LoginMobileRequest request)
        //{
        //    var result = await _authService.LoginUser(request);

        //    return Ok(result);
        //}

        ///// <summary>
        /////     Odnawia access token na podstawie refresh token
        ///// </summary>
        //[HttpPost("useToken")]
        //[ProducesResponseType(typeof(UseRefreshTokenResultDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult> RefreshAccessToken(UseRefreshTokenRequest request)
        //{
        //    if (_authService.IsTokenValid(request.AccessToken))
        //    {
        //        return Ok(new UseRefreshTokenResultDto
        //        {
        //            AccessToken = request.AccessToken
        //        });
        //    }

        //    var token = await _authService.RefreshAccessToken(request);

        //    return Ok(token);
        //}

        ///// <summary>
        /////     Usuwa refresh token z bazy
        ///// </summary>
        //[HttpPost("revokeToken")]
        //[Authorize(Roles = "User,Worker,Shelter,SuperAdmin,Admin")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult> RevokeToken([FromBody] TokenRequest request)
        //{
        //    await _authService.RevokeToken(request);
        //    return NoContent();
        //}

        
    }
}
