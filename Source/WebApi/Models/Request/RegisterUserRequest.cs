using System.ComponentModel.DataAnnotations;

namespace DrakeLambert.Peerra.WebApi.Models.Request
{
    public class RegisterUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}