using System;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities
{
    /// <summary>
    /// An access token.
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// The token string.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The expiry time.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
