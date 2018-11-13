using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Controllers.Dto;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DrakeLambert.Peerra.WebApi.Controllers
{
    public class TokenController : ApiControllerBase
    {
        private readonly IWebUserTokenService _tokenService;

        public TokenController(IWebUserTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("access")]
        public async Task<IActionResult> GenerateTokensAsync([FromBody] UserCredentialsDto model)
        {
            var tokensResult = await _tokenService.GenerateTokensAsync(model.Username, model.Password, HttpContext.Connection.RemoteIpAddress.ToString());

            if (tokensResult.Failed)
            {
                return BadRequest(tokensResult.Error);
            }

            return Ok(tokensResult.Value);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokensAsync([FromBody] PlainTokensDto model)
        {
            var refreshResult = await _tokenService.RefreshTokensAsync(model.AccessToken, model.RefreshToken, HttpContext.Connection.RemoteIpAddress.ToString());

            if (refreshResult.Failed)
            {
                return BadRequest(refreshResult.Error);
            }

            return Ok(refreshResult.Value);
        }
    }
}
