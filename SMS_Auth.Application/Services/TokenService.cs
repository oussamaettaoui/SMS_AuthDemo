using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SMS_Auth.Application.IServices;
using SMS_Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SMS_Auth.Domain.Dtos;

namespace SMS_Auth.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSetting _jwtSetting;
        private readonly UserManager<User> _userManager;
        #region Constructor
        public TokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _jwtSetting = configuration.GetSection("Jwt").Get<JwtSetting>();
            _userManager = userManager;
        }

        #endregion
        #region Generate Token Method
        public async Task<string> GenerateJwtToken(User user)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            string userRole = userRoles.FirstOrDefault();
            #region Claims
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,userRole)
            };
            #endregion
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSetting.TokenExpiryInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool IsTokenExpired(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken? jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken is null)
                {
                    return true;
                }
                var expClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp);
                if (expClaim is null)
                {
                    return true;
                }
                DateTimeOffset expDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value));
                return expDateTime.UtcDateTime < DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        #endregion
    }
}
