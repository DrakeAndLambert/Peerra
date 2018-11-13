using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Controllers.Dto;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.Repositories;
using DrakeLambert.Peerra.WebApi.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakeLambert.Peerra.WebApi.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] NewUserDto model, [FromServices] IUserService userService)
        {
            var user = new User(model.Username, model.Email, model.Bio);

            var createResult = await userService.RegisterUserAsync(user, model.Password);

            if (createResult.Failed)
            {
                return BadRequest(createResult.Error);
            }

            return CreatedAtAction(nameof(GetUserInfoAsync), new CreatedAtLocationDto(Url.Action(nameof(GetUserInfoAsync))));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserInfoAsync([FromServices] IUserRepository userRepository)
        {
            var username = User.Identity.Name;
            var userResult = await userRepository.GetAsync(username);

            if (userResult.Failed)
            {
                return BadRequest(userResult.Error);
            }

            return Ok(userResult.Value);
        }
    }
}
