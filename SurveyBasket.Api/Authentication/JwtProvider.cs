using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _options = options;

        public (string token, int expiresIn) GenerateToken(ApplicationUser user)
        {
            //group of claims to be included in the JWT token   
            //claims are used to store information about the user and their roles, which can be used for authorization purposes.
            // claims may be id user id, username, email, Permissions roles, and other relevant information.


            Claim[] claims = [
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                ];

            // create key to make encoding and decoding of the JWT token

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //
            var expiresIn = _options.Value.ExpiryMinutes; // 30 minutes


            var token = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: signingCredentials
                );

            return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: expiresIn * 60);
        }

        public string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                  IssuerSigningKey = symetricSecurityKey,
                  ValidateIssuerSigningKey=true,
                  ValidateIssuer=false,
                  ValidateAudience = false,
                  ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
               return jwtToken.Claims.First(x=>x.Type == JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
