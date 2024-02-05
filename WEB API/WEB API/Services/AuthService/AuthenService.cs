using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WEB_API.Dtos;
using WEB_API.Dtos.Auth;
using WEB_API.Dtos.Contact;
using WEB_API.Dtos.Register;
using WEB_API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WEB_API.Services.AuthService
{
    public class AuthenService : IAuthenService
    {
        private readonly ContactContext _dbContext;
        private readonly UserManager<ApplicationUser> _usermanger;
       private readonly IConfiguration _configuration;
        public AuthenService(ContactContext dbContext, IConfiguration config, UserManager<ApplicationUser> usermanger)
        {
           _dbContext = dbContext;
           _usermanger = usermanger;
          _configuration = config;
        }
        public async Task<ServerBaseReponse<AuthResponse>> login(AuthGmailRequest request)
        {
            var results = new ServerBaseReponse<AuthResponse>();
            try
            {
                if (request.gmail_token == null)
                {
                    results.Data = null;
                    results.Success = false;
                    results.Message = "NotOk";
                    return results;
                }
                var checkedUser = await _usermanger.Users.FirstOrDefaultAsync(x => x.GmailToken.Equals(request.gmail_token));
                if(checkedUser != null)
                {
                    var authresponse = GenerateToken(checkedUser);
                    results.Data = authresponse;
                    results.Success = true;
                    results.Message = "OK";
                }
                else
                {
                    checkedUser = new ApplicationUser
                    {
                        Email = "demo@gmail.com",
                    };
                    var authresponse = GenerateToken(checkedUser);
                    authresponse.isNew = true;
                    results.Data = authresponse;
                    results.Success = true;
                    results.Message = "OK";
                }
            }
            catch (Exception ex)
            {
                results.Data = null;
                results.Success = true;
                results.Message = "OK";
            }
            return results;
        }

        public async Task<ServerBaseReponse<RegisterModelResponse>> registerAccount(RegisterModelRequest request)
        {
            var results = new ServerBaseReponse<RegisterModelResponse>();
            var newUser = new ApplicationUser
            {
                UserName = request.email,
                Email = request.email,
                GmailToken = request.gmail_token,
                FullName = request.fullname,
                PhoneNumber = request.phoneNumber
            };
            var checkAdd = await _usermanger.CreateAsync(newUser);
            if (checkAdd.Succeeded)
            {
                var data = new RegisterModelResponse
                {
                    email = request.email,
                    phoneNumber = request.phoneNumber,
                    fullname = request.fullname,
                };
                results.Data = data;
                results.Success = true;
                results.Message ="OK";  
                return results;
            }
            else{

            }
            results.Data = null;
            results.Success = false;
            results.Message = "NotOK";
            return results;
        }
        private  AuthResponse GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:key").Value));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                issuer: _configuration.GetSection("Jwt:isuser").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return new AuthResponse
            {
                AccessToken = tokenString,
                isNew = false,
            }; 
        }
    }
}
