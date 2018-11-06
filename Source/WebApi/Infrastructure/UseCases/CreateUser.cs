using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.UseCases;
using Microsoft.AspNetCore.Identity;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.UseCases
{
    public class CreateUser : ICreateUser
    {
        private readonly UserManager<Peer> _userManager;

        public CreateUser(UserManager<Peer> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> HandleAsync(string username, string password)
        {
            Guard.Against.Null(username, nameof(username));
            Guard.Against.Null(password, nameof(password));

            var newUser = new Peer
            {
                UserName = username
            };

            var createUserResult = await _userManager.CreateAsync(newUser, password);

            if (!createUserResult.Succeeded)
            {
                return (false, createUserResult.Errors.Select(error => error.Description));
            }

            return (true, null);
        }
    }
}