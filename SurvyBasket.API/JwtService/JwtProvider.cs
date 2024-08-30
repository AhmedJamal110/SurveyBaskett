
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.ApI.JwtService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.API.JwtService
{
    public class JwtProvider : IJwtProvider
    {
      
                private readonly JwtOption _options;    
                private readonly SymmetricSecurityKey _key; 

        public JwtProvider( IOptions<JwtOption> options)
        {
            _options = options.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        }
        public async Task<(string token, int expireIn)> CreateTokenAsync(ApplicationUser user)
        {
            Claim[] claim =
                 [
                     new(JwtRegisteredClaimNames.Sub, user.Id),
                     new(JwtRegisteredClaimNames.Email, user.Email!),
                     new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                     new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                     new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 ];

          
            var token = new JwtSecurityToken
            (
                 claims: claim,
                issuer: _options.ValidIssuer,
                audience: _options.ValidAudiance,
                signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256),
               expires: DateTime.UtcNow.AddMinutes(_options.ExpireMinutes)
            );

            return (token: new JwtSecurityTokenHandler().WriteToken(token), expireIn: _options.ExpireMinutes * 60);
       }


        public string? VaildateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = _key,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

            }

            catch
            {
                return null;
            }

        }
    }
}
