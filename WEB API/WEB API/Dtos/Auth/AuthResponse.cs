using WEB_API.Dtos.Register;
using WEB_API.Models;

namespace WEB_API.Dtos.Auth
{
    public class AuthResponse
    {
        RegisterModelResponse registerModel { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public bool isNew { get; set; } = false;
    }
}
