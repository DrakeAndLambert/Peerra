using System;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.ExternalServices;
using DrakeLambert.Peerra.WebApi.Core.Specifications;

namespace DrakeLambert.Peerra.WebApi.Core.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IAsyncRepository<UserSkill> _userSkillrepository;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IAsyncRepository<User> userRepository, IAsyncRepository<UserSkill> userSkillrepository, ILogger<UserRepository> logger) : base(userRepository)
        {
            _userSkillrepository = userSkillrepository;
            _logger = logger;
        }

        public override async Task<Result<User>> AddAsync(User entity)
        {
            Guard.Against.Null(entity, nameof(entity));

            _logger.LogInformation("Adding user {username}.", entity.Username);

            var userNameTakenResult = await _entityRepository.AnyAsync(new UserWithUsername(entity.Username));

            if (userNameTakenResult.Failed)
            {
                _logger.LogWarning("Error determining if username {username} has conflicts.", entity.Username);
                return Result<User>.Fail($"Error determining if username {entity.Username} has conflicts.", userNameTakenResult);
            }

            if (userNameTakenResult.Value)
            {
                _logger.LogWarning("Username {username} has conflicts.", entity.Username);
                return Result<User>.Fail($"Username {entity.Username} has conflicts.");
            }

            var newUserResult = await _entityRepository.AddAsync(entity);

            if (newUserResult.Failed)
            {
                _logger.LogWarning("Error adding user {username}.", entity.Username);
                return Result<User>.Fail($"Error adding user {entity.Username}.", newUserResult);
            }

            _logger.LogInformation("Added user {username}.", entity.Username);
            return Result<User>.Success(newUserResult.Value);
        }

        public override async Task<Result> DeleteAsync(User entity)
        {
            Guard.Against.Null(entity, nameof(entity));

            _logger.LogInformation("Deleting user {username}.", entity.Username);

            var deleteResult = await _entityRepository.DeleteAsync(entity);

            if (deleteResult.Failed)
            {
                _logger.LogWarning("Error deleting user {username}.", entity.Username);
                return Result.Fail($"Error deleting user {entity.Username}.", deleteResult);
            }

            var userSkillsDeleteResult = await _userSkillrepository.DeleteRangeAsync(new UserSkillOfUser(entity.Username));

            if (userSkillsDeleteResult.Failed)
            {
                _logger.LogWarning("Error deleting userSkills for user {username}. User has been deleted.", entity.Username);
                return Result.Fail($"Error deleting userSkills for user {entity.Username}.", userSkillsDeleteResult);
            }

            throw new NotImplementedException("Add dependent entity deletion here.");

            return Result.Success();
        }

        public override async Task<Result> DeleteAsync(object id)
        {
            Guard.Against.Null(id, nameof(id));

            _logger.LogInformation("Deleting user {username}.", id);

            var userResult = await GetAsync(id);

            if (userResult.Failed)
            {
                _logger.LogWarning("Could not delete user {username}.", id);
                return Result.Fail($"Could not delete user {id}.", userResult);
            }

            throw new NotImplementedException("Add dependent entity deletion here.");

            return Result.Success();
        }

        public override async Task<Result> DeleteRangeAsync(ISpecification<User> specification)
        {
            var specificationName = specification.GetType().Name;
            _logger.LogInformation("Deleting users with specification {specification}.", specificationName);

            var matchingUsersResult = await _entityRepository.GetRangeAsync(specification);

            if (matchingUsersResult.Failed)
            {
                _logger.LogWarning("Error matching users to delete with specification {specification}.", specificationName);
                return Result.Fail($"Error matching users to delete with specification {specificationName}.", matchingUsersResult);
            }

            var deleteUsersResult = await _entityRepository.DeleteRangeAsync(matchingUsersResult.Value);

            if (deleteUsersResult.Failed)
            {
                _logger.LogWarning("Error deleting users with specification {specification}.", specificationName);
                return Result.Fail($"Error deleting users with specification {specificationName}.", deleteUsersResult);
            }

            var userNames = matchingUsersResult.Value.Select(user => user.Username).ToList();

            var userSkillsDeleteResult = await _userSkillrepository.DeleteRangeAsync(new UserSkillsOfUsers(userNames));

            if (userSkillsDeleteResult.Failed)
            {
                _logger.LogWarning("Error deleting userSkills for users with specification {specification}.", specificationName);
                return Result.Fail($"Error deleting userSkills for users with specification {specificationName}.", userSkillsDeleteResult);
            }

            throw new NotImplementedException("Add dependent entity deletion here.");

            return Result.Success();
        }
    }
}
