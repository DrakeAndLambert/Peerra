using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities
{
    public class WebUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
