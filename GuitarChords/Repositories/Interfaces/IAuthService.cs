using GuitarChords.Dtos;
using GuitarChords.Dtos.Requests;
using GuitarChords.Models;
using GuitarChords.Results;
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
