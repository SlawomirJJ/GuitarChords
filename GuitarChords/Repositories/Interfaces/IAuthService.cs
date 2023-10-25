using GuitarChords.Models;
using GuitarChords.Models.Dtos;
using GuitarChords.Models.Dtos.Requests;
using NuGet.Protocol.Plugins;

namespace GuitarChords.Repositories.Interfaces
{
    public interface IAuthService
    {
        Task <Status> Login(LoginRequest request);
        Task<Status> Registration(RegistrationDto request);
        Task Logout();
    }
}
