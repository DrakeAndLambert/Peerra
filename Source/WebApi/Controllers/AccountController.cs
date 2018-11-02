using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Infrastructure;
using DrakeLambert.Peerra.WebApi.Infrastructure.Services;
using DrakeLambert.Peerra.WebApi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DrakeLambert.Peerra.WebApi.Controllers
{
    public class AccountController : ApiControllerBase
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUserRequest">The new user's username and password.</param>
        /// <param name="userService">The UserService to register the user with.</param>
        /// <returns>200 if success, 400 otherwise.</returns>
        /// <response code="200">The user was created successfully.</response>
        /// <response code="400">The user could not be created. Returns an error message.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest registerUserRequest, [FromServices] UserService userService)
        {
            try
            {
                await userService.CreateAsync(registerUserRequest.Username, registerUserRequest.Password);

                return Ok();
            }
            catch (CreateUserException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}