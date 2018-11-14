using System;

namespace DrakeLambert.Peerra.WebApi2.Core.Entities
{
    public class UserSkill : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid SkillId { get; set; }
    }
}
