using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.Core.Data
{
    public interface IAsyncRepository<T, TKey> where T : IEntity<TKey>
    {
        Task<T> GetByIdAsync(TKey id);

        Task<IEnumerable<T>> ListAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}