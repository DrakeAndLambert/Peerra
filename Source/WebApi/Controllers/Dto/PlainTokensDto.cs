namespace DrakeLambert.Peerra.WebApi.Controllers.Dto
{
    /// <summary>
    /// A plain text access token and refresh token.
    /// </summary>
    public class PlainTokensDto
    {
        /// <summary>
        /// The access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        public PlainTokensDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

    }
}
