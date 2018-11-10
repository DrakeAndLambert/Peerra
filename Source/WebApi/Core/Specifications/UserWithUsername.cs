using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.Core.Specifications
{
    public class UserWithUsername : BaseSpecification<User>
    {
        public UserWithUsername(string username) : base(user => user.Username == username)
        { }
    }
}
