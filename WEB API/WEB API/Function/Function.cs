using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WEB_API.Dtos.Auth;
using WEB_API.Models;

namespace WEB_API.Function
{
    public static class Function
    {/*
        public static AuthResponse GenerateToken(ApplicationUser user)
        {
            /*
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("9NNUmLnvq9+fYGUMiqwZZphV+MUKJozTG4B8qnvmfoI=");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Name, user.F),


                    // Add additional claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(8), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return AuthResponse with the accessToken and expiration
            return new AuthResponse
            {
                AccessToken = tokenString,
                Expiration = tokenDescriptor.Expires ?? DateTime.MinValue,
                isNew = false,
            }; 
            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            SigningCredentials signingCredentials = null;
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                issuer: _c
        }*/

    }
}
