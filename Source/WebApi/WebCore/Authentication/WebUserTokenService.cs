using System;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.Core.ExternalServices;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    public class WebUserTokenService : IWebUserTokenService
    {
        private readonly IWebUserRepository _userRepository;
        private readonly ILogger<WebUserTokenService> _logger;
        private readonly ITokenFactory _tokenFactory;
        private readonly ITokenValidator _tokenValidator;

        public WebUserTokenService(IWebUserRepository userRepository, ILogger<WebUserTokenService> logger, ITokenFactory tokenFactory, ITokenValidator tokenValidator)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenFactory = tokenFactory;
            _tokenValidator = tokenValidator;
        }

        public async Task<Result<(AccessToken, RefreshToken)>> GenerateTokensAsync(string username, string password, string ipAddress)
        {
            Guard.Against.Null(username, nameof(username));
            Guard.Against.Null(password, nameof(password));
            Guard.Against.Null(ipAddress, nameof(ipAddress));

            _logger.LogInformation("Generating tokens for {username} at address {ipAddress}.", username, ipAddress);

            var userResult = await _userRepository.GetUserAsync(username);

            if (userResult.Failed)
            {
                _logger.LogWarning("Error generating tokens for user {username}. Couldn't look up user.", username);
                return Result<(AccessToken, RefreshToken)>.Fail($"Couldn't look up user.", userResult);
            }

            var user = userResult.Value;

            if (user == null)
            {
                _logger.LogWarning("Token generation for {username} failed. Username not found.", username);
                return Result<(AccessToken, RefreshToken)>.Fail("Invalid username or password.");
            }

            var passwordResult = await _userRepository.CheckPasswordAsync(user, password);

            if (passwordResult.Failed)
            {
                _logger.LogWarning("Error generating tokens for {username}. Password lookup failed.", username);
                return Result<(AccessToken, RefreshToken)>.Fail($"Password lookup failed.", passwordResult);
            }

            if (!passwordResult.Value)
            {
                _logger.LogWarning("Token generation for {username}. Password was incorrect.", username);
                return Result<(AccessToken, RefreshToken)>.Fail("Invalid username or password", passwordResult);
            }

            var accessToken = _tokenFactory.GenerateAccessToken(user.UserName);

            var refreshToken = _tokenFactory.GenerateRefreshToken(user.UserName, ipAddress);

            user.RefreshTokens.Add(refreshToken);

            var updateResult = await _userRepository.UpdateAsync(user);

            foreach (var token in user.RefreshTokens)
            {
                _logger.LogWarning(Newtonsoft.Json.JsonConvert.SerializeObject(token));
            }

            if (updateResult.Failed)
            {
                return Result<(AccessToken, RefreshToken)>.Fail("Refresh token conflict. Try again.", updateResult);
            }

            return Result<(AccessToken, RefreshToken)>.Success((accessToken, refreshToken));
        }

        public async Task<Result<(AccessToken, RefreshToken)>> RefreshTokensAsync(string accessToken, string refreshToken, string ipAddress)
        {
            Guard.Against.Null(accessToken, nameof(accessToken));
            Guard.Against.Null(refreshToken, nameof(refreshToken));
            Guard.Against.Null(ipAddress, nameof(ipAddress));

            _logger.LogInformation("Refreshing tokens for user at address {ipAddress}", ipAddress);

            var tokenIsValid = _tokenValidator.ValidateToken(accessToken, out var principal, validateLifetime: false);

            if (!tokenIsValid)
            {
                return Result<(AccessToken, RefreshToken)>.Fail("Invalid access token.");
            }

            var username = principal.Identity.Name;

            var userResult = await _userRepository.GetUserAsync(username);

            if (userResult.Failed)
            {
                _logger.LogWarning("Error refreshing tokens for user {username}. Couldn't look up user.", username);
                return Result<(AccessToken, RefreshToken)>.Fail($"Couldn't look up user.", userResult);
            }

            var user = userResult.Value;

            if (user == null)
            {
                _logger.LogWarning("Token refresh for {username} failed. Username not found.", username);
                return Result<(AccessToken, RefreshToken)>.Fail("Invalid access token.");
            }

            var matchedRefreshToken = user.RefreshTokens.FirstOrDefault(token => token.Token == refreshToken);

            if (matchedRefreshToken == null)
            {
                _logger.LogWarning("Token refresh for {username} failed. No matching refresh token.", username);
                return Result<(AccessToken, RefreshToken)>.Fail("Invalid refresh token.");
            }

            if (matchedRefreshToken.IpAddress != ipAddress)
            {
                _logger.LogWarning("Token refresh for {username} failed. IP address changed.", username);
                return Result<(AccessToken, RefreshToken)>.Fail("IP address has changed. Please login again.");
            }

            user.RefreshTokens.Remove(matchedRefreshToken);

            if (matchedRefreshToken.Expires > DateTime.UtcNow)
            {
                await _userRepository.UpdateAsync(user);
                _logger.LogWarning("Token refresh for {username} failed. Refresh token is expired.", username);
                return Result<(AccessToken, RefreshToken)>.Fail("Refresh token is expired. Please login again.");
            }

            var newAccessToken = _tokenFactory.GenerateAccessToken(user.UserName);
            var newRefreshToken = _tokenFactory.GenerateRefreshToken(user.UserName, ipAddress);

            user.RefreshTokens.Add(newRefreshToken);

            await _userRepository.UpdateAsync(user);

            return Result<(AccessToken, RefreshToken)>.Success((newAccessToken, newRefreshToken));
        }
    }
}
