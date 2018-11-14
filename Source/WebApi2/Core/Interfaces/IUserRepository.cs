using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi2.Core.Entities;
using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi2.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> AddUser(User user);

        Task<Result<User>> GetUserById(Guid id);

        Task<Result> DeleteUser(Guid id);
    }
}
