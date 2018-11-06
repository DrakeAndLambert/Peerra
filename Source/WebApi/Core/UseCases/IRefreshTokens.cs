using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.Core.UseCases
{
    public interface IRefreshTokens
    {
        Task<(AccessToken, RefreshToken)> Handle(string accessToken, string refreshToken, string ipAddress);
    }
}