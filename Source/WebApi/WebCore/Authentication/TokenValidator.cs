using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DrakeLambert.Peerra.WebApi.Core.ExternalServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    public class TokenValidator : ITokenValidator
    {
        private readonly AuthOptions _options;
        private readonly ILogger<TokenValidator> _logger;

        public TokenValidator(IOptions<AuthOptions> options, ILogger<TokenValidator> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public TokenValidationParameters DefaultTokenValidationParameters => new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _options.SecurityKey,
            ValidateLifetime = true,
            ValidateActor = false
        };

        public bool ValidateToken(string accessToken, out ClaimsPrincipal principal, bool validateLifetime = true)
        {
            _logger.LogInformation("Validating token.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = DefaultTokenValidationParameters;
            validationParameters.ValidateLifetime = validateLifetime;

            try
            {
                principal = tokenHandler.ValidateToken(accessToken, validationParameters, out _);
                return true;
            }
            catch (SecurityTokenException)
            {
                _logger.LogWarning("Token validation failed.");
                principal = null;
                return false;
            }
        }
    }
}
