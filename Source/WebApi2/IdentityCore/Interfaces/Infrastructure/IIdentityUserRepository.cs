using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi2.IdentityCore.Interfaces.Infrastructure
{
    public interface IIdentityUserRepository
    {
        Task<Result> RegisterUserAsync(string username, string password);

        Task<Result> DeleteUserAsync(string username);

        Task<Result<bool>> CheckPasswordAsync(string username, string password);
    }
}
