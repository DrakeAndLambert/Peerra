using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi2.IdentityCore.Entities;
using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi2.IdentityCore.Interfaces
{
    public interface IUserTokenService
    {
        Task<Result<(AccessToken, RefreshToken)>> CreateAndSaveTokensAsync(string username, string password, string ipAddress);

        Task<Result<(AccessToken, RefreshToken)>> ExchangeAndSaveTokensAsync(string accessToken, string refreshToken, string ipAddress);
    }
}
