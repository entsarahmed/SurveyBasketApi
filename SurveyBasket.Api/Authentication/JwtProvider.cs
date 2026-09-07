using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication
{
    public class JwtProvider : IJwtProvider
    {
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

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TYWT5taxv+JUqPfjDGyfMwCNNAQy0G50q5rGP0GwK/8="));

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //
            var expiresIn = 30; // 30 minutes


            var token = new JwtSecurityToken(
                issuer: "SurveyBasketApp",
                audience: "SurveyBasketApp Users",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: signingCredentials
                );

            return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: expiresIn);
        }
    }
}
