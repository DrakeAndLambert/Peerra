using System;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public DateTimeOffset ExpireTime { get; set; }

        public string IpAddress { get; set; }
    }
}