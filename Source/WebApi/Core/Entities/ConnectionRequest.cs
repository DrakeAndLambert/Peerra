/*
+- Register user with username, email, bio
+- Delete user and user data
+- Add skills to profile
+- View skills of user
+- request to connect to other user
+- accept connection from other user
+- view connections
+- delete connection

# As Mentoree
+- browse all mentors by skill
+- create skill request
    +- see users with requested skills
    +- close skill request

# As Mentor
+- browse skill requests
 */



namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    public class Connection : IEntity
    {
        public User Requestor { get; set; }

        public bool RequestorIsMentor { get; set; }

        public User Target { get; set; }

        public string Message { get; set; }

        public bool Accepted { get; set; }

        public bool Denied { get; set; }
    }
}
