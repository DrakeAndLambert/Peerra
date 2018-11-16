using System.ComponentModel.DataAnnotations;

namespace DrakeLambert.Peerra.WebApi.Web.Dto
{
    public class NewUserDto : UserCredentialsDto
    {
        [Required]
        public string Email { get; set; }

        public string Bio { get; set; }

    }
}
