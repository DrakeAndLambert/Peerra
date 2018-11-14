using Ardalis.GuardClauses;

namespace DrakeLambert.Peerra.WebApi2.Core.Entities
{
    public class User : BaseEntity
    {
        private string _username;

        private string _email;

        private string _bio;

        public string Username
        {
            get => _username;
            set
            {
                Guard.Against.Null(value, nameof(Username));
                _username = value.ToLower();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                Guard.Against.Null(value, nameof(Email));
                _email = value.ToLower();
            }
        }

        public string Bio
        {
            get => _bio;
            set
            {
                Guard.Against.Null(value, nameof(Bio));
                _bio = value;
            }
        }
    }
}
