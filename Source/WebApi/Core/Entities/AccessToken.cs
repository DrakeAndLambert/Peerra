using System;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTimeOffset ExpireTime { get; set; }
    }
}