using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities
{
    public class WebUser : IdentityUser
    {
        private List<RefreshToken> _refreshTokens;

        public List<RefreshToken> RefreshTokens
        {
            get
            {
                if (_refreshTokens == null)
                {
                    _refreshTokens = new List<RefreshToken>();
                }
                return _refreshTokens;
            }
            set => _refreshTokens = value;
        }
    }
}
