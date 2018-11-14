using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi2.Core.Entities;

namespace DrakeLambert.Peerra.WebApi2.Core.Interfaces.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification);
        
        Task<TEntity> AddAsync(TEntity entity);
        
        Task UpdateAsync(TEntity entity);
        
        Task DeleteAsync(TEntity entity);
        
        Task<int> CountAsync(ISpecification<TEntity> spec);
    }
}
