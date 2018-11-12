using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    public interface ITokenFactory
    {
        RefreshToken GenerateRefreshToken(string username, string ipAddress);
        AccessToken GenerateAccessToken(string username);
    }
}
