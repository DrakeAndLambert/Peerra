using DrakeLambert.Peerra.WebApi2.IdentityCore.Entities;

namespace DrakeLambert.Peerra.WebApi2.IdentityCore.Interfaces
{
    public interface ITokenFactory
    {
        AccessToken GenerateAccessToken(string username);

        RefreshToken GenerateRefreshToken(string username, string ipAddress);
    }
}
