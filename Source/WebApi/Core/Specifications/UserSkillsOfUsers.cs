using System.Collections.Generic;
using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.Core.Specifications
{
    public class UserSkillsOfUsers : BaseSpecification<UserSkill>
    {
        public UserSkillsOfUsers(List<string> usernames) : base(userSkill => usernames.Contains(userSkill.User.Username))
        { }
    }
}
