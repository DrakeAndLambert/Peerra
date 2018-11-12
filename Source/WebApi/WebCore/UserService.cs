using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.ExternalServices;
using DrakeLambert.Peerra.WebApi.Core.Repositories;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;

namespace DrakeLambert.Peerra.WebApi.WebCore
{
    public class UserService : IUserService
    {
        private readonly IWebUserRepository _webUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IWebUserRepository webUserRepository, IUserRepository userRepository, ILogger<UserService> logger)
        {
            _webUserRepository = webUserRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result> DeleteUserAsync(User user)
        {
            Guard.Against.Null(user, nameof(user));

            _logger.LogInformation("Deleting user {username}.", user.Username);

            var userDeleteResult = await _userRepository.DeleteAsync(user.Username);

            if (userDeleteResult.Failed)
            {
                _logger.LogError("Error deleting user {username}", null, user.Username);
                return Result.Fail($"Error deleting user {user.Username}.", userDeleteResult);
            }

            var webUserResult = await _webUserRepository.GetUserAsync(user.Username);

            if (webUserResult.Failed)
            {
                _logger.LogError("Error deleting user {username}.", null, user.Username);
                return Result.Fail($"Error deleting user {user.Username}.", webUserResult);
            }

            var webUser = webUserResult.Value;

            if (webUser == null)
            {
                _logger.LogWarning("Error deleting user {username}. User not found.", user.Username);
                return Result.Fail($"Error deleting user {user.Username}. User not found.");
            }

            var webUserDeleteResult = await _webUserRepository.DeleteUserAsync(webUser);

            if (webUserDeleteResult.Failed)
            {
                _logger.LogError("Error deleting user {username}.", null, user.Username);
                return Result.Fail($"Error deleting user {user.Username}.", webUserResult);
            }

            return Result.Success();
        }

        public async Task<Result> RegisterUserAsync(User user, string password)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.Null(password, nameof(password));

            _logger.LogInformation("Registering new user {username}.", user.Username);

            var webUser = new WebUser
            {
                UserName = user.Username
            };

            var webUserResult = await _webUserRepository.AddUserAsync(webUser, password);

            if (webUserResult.Failed)
            {
                _logger.LogWarning("Error registering user {username}.", user.Username);
                return Result.Fail("Error registering user.", webUserResult);
            }

            var userResult = await _userRepository.AddAsync(user);

            if (userResult.Failed)
            {
                var findWebUserResult = await _webUserRepository.GetUserAsync(user.Username);

                if (findWebUserResult.Failed)
                {
                    _logger.LogError("Error recovering from user generation error. Database may have un-synced user information for {username}.", null, user.Username);
                }
                else
                {
                    await _webUserRepository.DeleteUserAsync(findWebUserResult.Value);
                    _logger.LogWarning("Error registering user {username}.", user.Username);
                }

                return Result.Fail("Error registering user.", userResult);
            }

            return Result.Success();
        }
    }
}
