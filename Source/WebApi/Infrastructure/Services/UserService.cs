using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.Data;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Services
{
    public class UserService
    {
        private readonly UserManager<Identity.IdentityUser> _userManager;

        private readonly IAsyncRepository<Peer, Guid> _peerRepository;

        public UserService(UserManager<Identity.IdentityUser> userManager, IAsyncRepository<Peer, Guid> peerRepository)
        {
            _userManager = userManager;
            _peerRepository = peerRepository;
        }

        public async Task RegisterAsync(string username, string password)
        {
            Guard.Against.Null(username, nameof(username));
            Guard.Against.Null(password, nameof(password));

            var newAppUser = new Identity.IdentityUser
            {
                UserName = username
            };

            var createUserResult = await _userManager.CreateAsync(newAppUser, password);

            if (!createUserResult.Succeeded)
            {
                throw new RegisterUserException(createUserResult);
            }

            var newPeer = new Peer(newAppUser.Id);

            try
            {
                await _peerRepository.AddAsync(newPeer);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                await _userManager.DeleteAsync(newAppUser);

                throw new RegisterUserException($"The username '{username}' is already in use.", exception);
            }
        }
    }
}