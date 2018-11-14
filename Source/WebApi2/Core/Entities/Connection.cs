using System;

namespace DrakeLambert.Peerra.WebApi2.Core.Entities
{
    public class Connection : BaseEntity
    {
        public Guid RequestorId { get; set; }

        public bool RequestorIsMentor { get; set; }

        public Guid TargetId { get; set; }

        public string Message { get; set; }

        public bool Accepted { get; set; }

        public bool Denied { get; set; }
    }
}
