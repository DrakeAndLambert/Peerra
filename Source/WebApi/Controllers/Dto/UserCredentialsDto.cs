using System.ComponentModel.DataAnnotations;

namespace DrakeLambert.Peerra.WebApi.Controllers.Dto
{
    public class UserCredentialsDto
    {
        /// <summary>
        /// The username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
