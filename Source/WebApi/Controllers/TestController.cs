using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Controllers
{
    public class TestController : ApiControllerBase
    {
        [HttpPost("make")]
        public async Task<IActionResult> MakeUserAsync([FromServices] Infrastructure.Data.ApplicationDbContext context)
        {
            var user = new WebCore.Authentication.Entities.WebUser
            {
                UserName = "test"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("addToken")]
        public async Task<IActionResult> AddTokens([FromQuery] string id, [FromServices] Infrastructure.Data.ApplicationDbContext context)
        {
            var user = await context.Users.FindAsync(id);
            // await context.Entry(user).Collection(u => u.RefreshTokens).LoadAsync();
            var token = new RefreshToken
            {
                Expires = DateTime.UtcNow.AddDays(20),
                IpAddress = "1.1.1.1",
                Token = "Test token",
                Username = "test"
            };
            user.RefreshTokens.Add(token);
            await context.SaveChangesAsync();
            return Ok(token);
        }

        [HttpGet("all")]
        public async Task<IActionResult> All([FromServices] Infrastructure.Data.ApplicationDbContext context)
        {
            return Ok(context.Users.Include(u => u.RefreshTokens));
        }

        [HttpGet]
        public async Task<IActionResult> Retrieve([FromServices] Infrastructure.Data.ApplicationDbContext context)
        {
            var user = await context.Users.FindAsync("test");
            return Ok(user);
        }
    }
}
