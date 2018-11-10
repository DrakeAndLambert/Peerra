namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    using Ardalis.GuardClauses;

    public class User : IEntity
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

        public User(string username, string email, string bio)
        {
            Username = username;
            Email = email;
            Bio = bio;
        }
    }
}
