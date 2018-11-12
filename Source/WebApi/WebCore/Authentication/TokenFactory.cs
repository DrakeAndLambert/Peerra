using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.ExternalServices;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    public class TokenFactory : ITokenFactory
    {
        private readonly AuthOptions _options;
        private readonly ILogger<TokenFactory> _logger;

        public TokenFactory(IOptions<AuthOptions> options, ILogger<TokenFactory> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public AccessToken GenerateAccessToken(string username)
        {
            Guard.Against.Null(username, nameof(username));

            _logger.LogInformation("Generating access token for {username}.", username);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                            {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(_options.AccessTokenLifetimeHours),
                SigningCredentials = new SigningCredentials(_options.SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return new AccessToken
            {
                Token = tokenHandler.CreateEncodedJwt(tokenDescriptor),
                Expires = tokenDescriptor.Expires.Value
            };
        }

        public RefreshToken GenerateRefreshToken(string username, string ipAddress)
        {
            Guard.Against.Null(username, nameof(username));
            Guard.Against.Null(ipAddress, nameof(ipAddress));

            _logger.LogInformation("Generating refresh token for {username} at address {ipAddress}.", username, ipAddress);

            var randomNumber = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(randomNumber);
            }

            return new RefreshToken
            {
                Expires = DateTime.UtcNow.AddDays(_options.RefreshTokenLifetimeDays),
                IpAddress = ipAddress,
                Username = username,
                Token = Convert.ToBase64String(randomNumber)
            };
        }
    }
}
