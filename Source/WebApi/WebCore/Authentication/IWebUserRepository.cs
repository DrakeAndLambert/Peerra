using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;

namespace DrakeLambert.Peerra.WebApi.WebCore.Authentication
{
    public interface IWebUserRepository
    {
        Task<Result> AddUserAsync(WebUser user, string password);
        Task<Result> DeleteUserAsync(WebUser user);
        Task<Result<WebUser>> GetUserAsync(string username);
        Task<Result<bool>> CheckPasswordAsync(WebUser user, string password);
        Task<Result> UpdateAsync(WebUser user);
    }
}
