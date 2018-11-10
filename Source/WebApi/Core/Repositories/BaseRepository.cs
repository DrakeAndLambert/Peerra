using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.Specifications;

namespace DrakeLambert.Peerra.WebApi.Core.Repositories
{
    public abstract class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : IEntity
    {
        protected readonly IAsyncRepository<TEntity> _entityRepository;

        public BaseRepository(IAsyncRepository<TEntity> repository)
        {
            _entityRepository = repository;
        }

        public virtual Task<Result<TEntity>> AddAsync(TEntity entity) => _entityRepository.AddAsync(entity);

        public virtual Task<Result<bool>> AnyAsync(ISpecification<TEntity> specification) => _entityRepository.AnyAsync(specification);

        public virtual Task<Result> DeleteAsync(TEntity entity) => _entityRepository.DeleteAsync(entity);

        public virtual Task<Result> DeleteAsync(object id) => _entityRepository.DeleteAsync(id);

        public virtual Task<Result> DeleteRangeAsync(ISpecification<TEntity> specification) => _entityRepository.DeleteRangeAsync(specification);

        public Task<Result> DeleteRangeAsync(IEnumerable<TEntity> entities) => _entityRepository.DeleteRangeAsync(entities);

        public virtual Task<Result<TEntity>> GetAsync(object id) => _entityRepository.GetAsync(id);

        public virtual Task<Result<List<TEntity>>> GetRangeAsync() => _entityRepository.GetRangeAsync();

        public virtual Task<Result<List<TEntity>>> GetRangeAsync(int startIndex, int count) => _entityRepository.GetRangeAsync(startIndex, count);

        public virtual Task<Result<List<TEntity>>> GetRangeAsync(ISpecification<TEntity> specification) => _entityRepository.GetRangeAsync(specification);

        public virtual Task<Result<List<TEntity>>> GetRangeAsync(ISpecification<TEntity> specification, int startIndex, int count) => _entityRepository.GetRangeAsync(specification, startIndex, count);

        public virtual Task<Result> UpdateAsync(TEntity entity) => _entityRepository.UpdateAsync(entity);
    }
}
