using System.ComponentModel.DataAnnotations;

namespace DrakeLambert.Peerra.WebApi2.Web.Dto
{
    public class NewUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string Bio { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
