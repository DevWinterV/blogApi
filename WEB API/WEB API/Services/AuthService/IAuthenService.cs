using WEB_API.Dtos;
using WEB_API.Dtos.Auth;
using WEB_API.Dtos.Contact;
using WEB_API.Dtos.Register;
using WEB_API.Models;

namespace WEB_API.Services.AuthService
{
    public interface IAuthenService
    {
        Task<ServerBaseReponse<AuthResponse>> login(AuthGmailRequest request);
        Task<ServerBaseReponse<RegisterModelResponse>> registerAccount(RegisterModelRequest request);

    }
}
