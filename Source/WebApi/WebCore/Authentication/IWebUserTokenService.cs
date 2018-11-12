using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    public interface IWebUserTokenService
    {
        /// <summary>
        /// Generates access and refresh tokens for a user and stores the refresh token.
        /// </summary>
        /// <returns>The tokens.</returns>
        Task<Result<(AccessToken, RefreshToken)>> GenerateTokensAsync(string username, string password, string ipAddress);

        /// <summary>
        /// Generates a new access and refresh token given an expired access token and a valid refresh token. Stores the new refresh token and deletes the old one.
        /// </summary>
        /// <returns>The new tokens.</returns>
        Task<Result<(AccessToken, RefreshToken)>> RefreshTokensAsync(string accessToken, string refreshToken, string ipAddress);
    }
}
