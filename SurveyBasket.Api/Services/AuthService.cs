using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Authentication;
using System.Security.Cryptography;

namespace SurveyBasket.Api.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;


        private readonly int _refreshTokenExpiryDays = 14;
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

            var (token, expiresIn) = _jwtProvider.GenerateToken(user);

            var refreshToken = GenerateRefreshToken();

            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration

            });
            await _userManager.UpdateAsync(user);

            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpiration);
        }

        

        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return null!;

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return null;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return null;

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration

            });
            await _userManager.UpdateAsync(user);
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, newRefreshToken, refreshTokenExpiration);


        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
