using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Infrastructure.Data;
using DrakeLambert.Peerra.WebApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure
{
    public class UserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public UserRepository(UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(string username, string password)
        {
            Guard.Against.Null(username, nameof(username));
            Guard.Against.Null(password, nameof(password));

            var newAppUser = new AppUser
            {
                UserName = username
            };

            var createUserResult = await _userManager.CreateAsync(newAppUser, password);

            if (!createUserResult.Succeeded)
            {
                throw new CreateUserException(createUserResult);
            }

            var newPeer = new Peer
            {
                Id = username
            };

            _appDbContext.Peers.Add(newPeer);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                var appUser = await _userManager.FindByNameAsync(username);
                await _userManager.DeleteAsync(appUser);

                throw new CreateUserException($"The username '{username}' is already in user.", exception);
            }
        }
    }
}