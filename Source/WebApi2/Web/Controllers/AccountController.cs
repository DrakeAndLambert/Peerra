using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.SharedKernel.Interfaces.Infrastructure;
using DrakeLambert.Peerra.WebApi2.Core.Entities;
using DrakeLambert.Peerra.WebApi2.Core.Interfaces;
using DrakeLambert.Peerra.WebApi2.Web.Dto;
using DrakeLambert.Peerra.WebApi2.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrakeLambert.Peerra.WebApi2.Web.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IWebUserService _webUserService;
        private readonly IUserService _userService;
        private readonly IAppLogger<AccountController> _logger;

        public AccountController(IWebUserService webUserService, IUserService userService, IAppLogger<AccountController> logger)
        {
            _webUserService = webUserService;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="newUser">The new user's information.</param>
        /// <returns>The new user or an error.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] NewUserDto newUser)
        {
            _logger.LogInformation("Registering user '{username}'.", newUser.Username);

            var appUser = new User
            {
                Username = newUser.Username,
                Bio = newUser.Bio,
                Email = newUser.Email
            };

            var addUserResult = await _webUserService.AddUser(appUser, newUser.Password);

            if (addUserResult.Failed)
            {
                _logger.LogWarning("New user with name '{username}', was invalid.", newUser.Username);
                return BadRequest(new ErrorDto(addUserResult));
            }

            return CreatedAtAction(nameof(Info), appUser);
        }

        /// <summary>
        /// Gets the user info for the currently logged in user.
        /// </summary>
        /// <returns>The user's info or an error.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Info()
        {
            var username = User.Identity.Name;
            _logger.LogInformation("Retrieving info for user '{username}'.", username);

            var userLookup = await _userService.GetUserAsync(username);

            if (userLookup.Failed)
            {
                return BadRequest(new ErrorDto(userLookup));
            }

            return Ok(userLookup.Value);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] UserCredentialsDto userCredentials)
        {
            throw new NotImplementedException();
        }
    }
}
