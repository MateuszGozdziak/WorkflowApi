using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkflowApi.Entities;
using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public class JwtTokenService : IJwtTokenService
    {

        private readonly JwtSettings _authSettings;

        public JwtTokenService(JwtSettings authSettings)
        {
            this._authSettings = authSettings;
        }
        public async Task<Tuple<string, DateTime>> GenerateJwt(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };

            var issuerAppSettings = _authSettings.jwtIssuer;
            double minutesAppSettings = _authSettings.jwtExpire;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresMinutes = DateTime.Now.AddMinutes(minutesAppSettings);

            var token = new JwtSecurityToken(issuerAppSettings,
                issuerAppSettings,
                claims,
                expires: expiresMinutes,
                signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();

            return Tuple.Create(tokenHandler.WriteToken(token), expiresMinutes);
        }
    }
}
