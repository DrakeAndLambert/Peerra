using System;

namespace DrakeLambert.Peerra.WebApi2.Core.Entities
{
    public class RequestedSkill
    {
        public Guid SkillId { get; set; }

        public Guid SkillRequestId { get; set; }
    }
}
