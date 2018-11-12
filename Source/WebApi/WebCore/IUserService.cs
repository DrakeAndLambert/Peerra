using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.WebCore
{
    public interface IUserService
    {
        Task<Result> RegisterUserAsync(User user, string password);
        Task<Result> DeleteUserAsync(User user);
    }
}
