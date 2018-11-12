using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    /// <summary>
    /// Options for user authentication.
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// The secret key to sign authentication tokens with.
        /// </summary>
        public string TokenSigningKey { get; set; }

        /// <summary>
        /// The lifetime of an access token in hours.
        /// </summary>
        public int AccessTokenLifetimeHours { get; set; }

        /// <summary>
        /// The lifetime of a refresh token in days.
        /// </summary>
        public int RefreshTokenLifetimeDays { get; set; }

        /// <summary>
        /// The SymmetricSecurityKey created from the TokenSigningKey.
        /// </summary>
        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSigningKey));
    }
}
