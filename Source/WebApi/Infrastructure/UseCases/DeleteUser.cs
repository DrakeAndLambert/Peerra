using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.UseCases;
using Microsoft.AspNetCore.Identity;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.UseCases
{
    public class DeleteUser : IDeleteUser
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUser(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(string username, string password)
        {
            Guard.Against.Null(username, nameof(username));
            Guard.Against.Null(password, nameof(password));

            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}