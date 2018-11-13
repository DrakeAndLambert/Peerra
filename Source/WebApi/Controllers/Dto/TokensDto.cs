using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;

namespace DrakeLambert.Peerra.WebApi.Controllers.Dto
{
    /// <summary>
    /// An access and refresh token.
    /// </summary>
    public class TokensDto
    {
        /// <summary>
        /// The access token.
        /// </summary>
        public AccessToken AccessToken { get; set; }

        /// <summary>
        /// The refresh token
        /// </summary>
        public RefreshToken RefreshToken { get; set; }

        /// <summary>
        /// Creates an instance.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        public TokensDto(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
