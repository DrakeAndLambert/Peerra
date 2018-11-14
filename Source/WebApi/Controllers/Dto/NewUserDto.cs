using System.ComponentModel.DataAnnotations;

namespace DrakeLambert.Peerra.WebApi.Controllers.Dto
{
    /// <summary>
    /// Contains the information necessary to create a new user.
    /// </summary>
    public class NewUserDto : UserCredentialsDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Bio { get; set; }
    }
}
