using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.Specifications;

namespace DrakeLambert.Peerra.WebApi.Core.Repositories
{
    public interface IAsyncRepository<TEntity> where TEntity : IEntity
    {
        // Read
        Task<Result<TEntity>> GetAsync(object id);
        Task<Result<List<TEntity>>> GetRangeAsync();
        Task<Result<List<TEntity>>> GetRangeAsync(int startIndex, int count);
        Task<Result<List<TEntity>>> GetRangeAsync(ISpecification<TEntity> specification);
        Task<Result<List<TEntity>>> GetRangeAsync(ISpecification<TEntity> specification, int startIndex, int count);
        Task<Result<bool>> AnyAsync(ISpecification<TEntity> specification);

        // Write
        Task<Result<TEntity>> AddAsync(TEntity entity);
        Task<Result> UpdateAsync(TEntity entity);
        Task<Result> DeleteAsync(TEntity entity);
        Task<Result> DeleteAsync(object id);
        Task<Result> DeleteRangeAsync(ISpecification<TEntity> specification);
        Task<Result> DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
