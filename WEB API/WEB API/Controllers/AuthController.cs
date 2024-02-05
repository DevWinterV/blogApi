using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WEB_API.Dtos;
using WEB_API.Dtos.Auth;
using WEB_API.Dtos.Contact;
using WEB_API.Dtos.Register;
using WEB_API.Models;
using WEB_API.Services.AuthService;
using WEB_API.Services.ContactsService;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenService _iAuthenService;
        private readonly ILogger<ContactController> _logger;

        public AuthController(ILogger<ContactController> logger, IAuthenService iAuthenService)
        {
            _iAuthenService = iAuthenService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ServerBaseReponse<AuthResponse>>> authGmail(AuthGmailRequest request)
        {
            var results = await _iAuthenService.login(request);
            return Ok(results);
        }

        [HttpPost]
        [Authorize]
        [Route("register")] // Remove "api/[controller]/" from the route
        public async Task<ActionResult<ServerBaseReponse<RegisterModelResponse>>> register(RegisterModelRequest request)
        {
            var results = await _iAuthenService.registerAccount(request);
            return Ok(results);
        }

    }
}
