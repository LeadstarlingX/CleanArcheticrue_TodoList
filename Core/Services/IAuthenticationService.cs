using System;
using Core.DTO;

namespace Core.Services
{
    public interface IAuthenticationService
    {
        Task<string?> Login(LoginUserDTO dto);

        Task<string?> RefreshToken();

        int RetrieveUserID();

        string AccessTokenString();


    }
}
