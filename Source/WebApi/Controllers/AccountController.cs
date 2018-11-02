using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Infrastructure;
using DrakeLambert.Peerra.WebApi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DrakeLambert.Peerra.WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest model, [FromServices] UserRepository userRepository)
        {
            try
            {
                await userRepository.CreateAsync(model.Username, model.Password);

                return Ok();
            }
            catch (CreateUserException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}