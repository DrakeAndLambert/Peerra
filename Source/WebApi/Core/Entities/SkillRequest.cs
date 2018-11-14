/*
+- Register user with username, email, bio
+- Delete user and user data
+- Add skills to profile
+- View skills of user
- request to connect to other user
- accept connection from other user
- view connections
- delete connection

# As Mentoree
- browse all mentors by skill
- create skill request
    - see users with requested skills
    - close skill request

# As Mentor
- browse skill requests
 */


using System.Collections.Generic;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{

    public class SkillRequest : IEntity
    {
        public User Requestor { get; set; }

        public List<Skill> RequestedSkills { get; set; }
    }
}
