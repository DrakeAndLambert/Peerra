using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    public interface ITokenValidator
    {
        TokenValidationParameters DefaultTokenValidationParameters { get; }
        bool ValidateToken(string accessToken, out ClaimsPrincipal principal, bool validateLifetime = true);
    }
}
