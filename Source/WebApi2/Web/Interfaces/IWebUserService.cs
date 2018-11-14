using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi2.Core.Entities;
using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi2.Web.Interfaces
{
    /// <summary>
    /// This service is specifically for maintaining changes between Core users and IdentityCore users.
    /// </summary>
    public interface IWebUserService
    {
        Task<Result> AddUser(User user, string password);

        Task<Result> DeleteUser(Guid id);
    }
}
