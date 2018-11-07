using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Data;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class EFRepository<T, TKey> : IAsyncRepository<T, TKey> where T : class, IEntity<TKey>
    {
        private readonly ApplicationDbContext _context;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}