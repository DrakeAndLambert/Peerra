using System.IdentityModel.Tokens.Jwt;
using DrakeLambert.Peerra.WebApi.SharedKernel.Interfaces.Infrastructure;
using DrakeLambert.Peerra.WebApi2.IdentityCore.Interfaces;
using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DrakeLambert.Peerra.WebApi2.IdentityCore.Services
{
    public class TokenValidator : ITokenValidator
    {
        private readonly IdentityOptions _options;
        private readonly IAppLogger<TokenValidator> _logger;

        public TokenValidationParameters DefaultTokenValidationParameters => new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _options.SecurityKey,
            ValidateLifetime = true,
            ValidateActor = false
        };

        public TokenValidator(IOptions<IdentityOptions> options, IAppLogger<TokenValidator> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public Result<string> ValidateAndGetUser(string accessToken)
        {
            _logger.LogInformation("Validating token.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = DefaultTokenValidationParameters;
            validationParameters.ValidateLifetime = false;

            try
            {
                var principal = tokenHandler.ValidateToken(accessToken, validationParameters, out _);
                return Result<string>.Success(principal.Identity.Name);
            }
            catch (SecurityTokenException)
            {
                _logger.LogWarning("Token validation failed.");
                return Result<string>.Fail("Invalid access token.");
            }
        }
    }
}
