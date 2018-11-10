namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    using Ardalis.GuardClauses;

    public class UserSkill : IEntity
    {
        private User _user;

        private Skill _skill;

        public User User
        {
            get => _user;
            set
            {
                Guard.Against.Null(value, nameof(User));
                _user = value;
            }
        }

        public Skill Skill
        {
            get => _skill;
            set
            {
                Guard.Against.Null(value, nameof(Skill));
                _skill = value;
            }
        }

        public UserSkill(User user, Skill skill)
        {
            User = user;
            Skill = skill;
        }
    }
}
