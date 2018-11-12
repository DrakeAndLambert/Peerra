using System;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities
{
    /// <summary>
    /// A refresh token.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// The token string.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The expiry time.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// The username for the user who owns the token.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The ipAddress that was used to generate the token.
        /// </summary>
        public string IpAddress { get; set; }
    }
}
