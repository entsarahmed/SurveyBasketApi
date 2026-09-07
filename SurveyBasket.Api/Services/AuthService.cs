


using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.Api.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        public async Task<AuthResponse?> GetTokenAsync(string email, string password ="12344321", CancellationToken cancellationToken = default)
        {
            ///check if the email (user)
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return null;

            ///check if  password are valid

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
                return null;
            
            
            ///Generate JWT token
           
           
            ///return the token and user information in AuthResponse
            
               
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30", 3600);

        }
    }
}
