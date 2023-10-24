using GuitarChords.Dtos.Requests;
using GuitarChords.Models;
using GuitarChords.Results;
using NuGet.Protocol.Plugins;

namespace GuitarChords.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUser(CreateUserRequest request);
        Task<LoginResultDto> LoginUser(CreateUserRequest request);
        string CreateAccessToken(User user);
        Task<UseRefreshTokenResultDto> RefreshAccessToken(UseRefreshTokenRequest request);
        string CreateRefreshToken();
        bool IsTokenValid(string token);
        Task RevokeToken(string request);
        Task ResetPassword(string request);
        Task SetNewPassword(ResetPasswordRequest resetPasswordRequest, string token);
    }
}
