using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class WebUserRepository : IWebUserRepository
    {
        private readonly UserManager<WebUser> _userManager;

        public WebUserRepository(UserManager<WebUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> AddUserAsync(WebUser user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                return Result.Fail(createResult.Errors.First().Description);
            }
            return Result.Success();
        }

        public async Task<Result<bool>> CheckPasswordAsync(WebUser user, string password)
        {
            return Result<bool>.Success(await _userManager.CheckPasswordAsync(user, password));
        }

        public async Task<Result> DeleteUserAsync(WebUser user)
        {
            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                return Result.Fail(deleteResult.Errors.First().Description);
            }
            return Result.Success();
        }

        public async Task<Result<WebUser>> GetUserAsync(string username)
        {
            return Result<WebUser>.Success(await _userManager.FindByNameAsync(username));
        }

        public async Task<Result> UpdateAsync(WebUser user)
        {
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return Result.Fail(updateResult.Errors.First().Description);
            }
            return Result.Success();
        }
    }
}
