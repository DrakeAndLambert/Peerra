using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.Core.Specifications
{
    public class UserSkillOfUser : BaseSpecification<UserSkill>
    {
        public UserSkillOfUser(string username) : base(userSkill => userSkill.User.Username == username)
        { }
    }
}
