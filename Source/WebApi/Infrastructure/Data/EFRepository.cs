using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Dto;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.Core.Repositories;
using DrakeLambert.Peerra.WebApi.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class EFRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApplicationDbContext _context;

        private DbSet<TEntity> _entitySet => _context.Set<TEntity>();

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<TEntity>> AddAsync(TEntity entity)
        {
            _entitySet.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return Result<TEntity>.Fail(e.Message);
            }
            return Result<TEntity>.Success(entity);
        }

        public async Task<Result<bool>> AnyAsync(ISpecification<TEntity> specification)
        {
            return Result<bool>.Success(await _entitySet.AnyAsync(specification.ComposedCriteria));
        }

        public async Task<Result> DeleteAsync(TEntity entity)
        {
            _entitySet.Remove(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return Result<TEntity>.Fail(e.Message);
            }
            return Result<TEntity>.Success(entity);
        }

        public async Task<Result> DeleteAsync(object id)
        {
            var entityResult = await GetAsync(id);
            if (entityResult.Failed)
            {
                return Result.Fail("Error finding entity.", entityResult);
            }
            _entitySet.Remove(entityResult.Value);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return Result.Fail(e.Message);
            }
            return Result.Success();
        }

        public async Task<Result> DeleteRangeAsync(ISpecification<TEntity> specification)
        {
            var entitiesResult = await GetRangeAsync(specification);

            if (entitiesResult.Failed)
            {
                return Result.Fail("Error finding entities.", entitiesResult);
            }

            return await DeleteRangeAsync(entitiesResult.Value);
        }

        public async Task<Result> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _entitySet.RemoveRange(entities);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return Result.Fail(e.Message);
            }
            return Result.Success();
        }

        public async Task<Result<TEntity>> GetAsync(object id)
        {
            var entity = await _entitySet.FindAsync(id);
            if (entity == null)
            {
                return Result<TEntity>.Fail("Entity not found.");
            }
            return Result<TEntity>.Success(entity);
        }

        public async Task<Result<List<TEntity>>> GetRangeAsync()
        {
            return Result<List<TEntity>>.Success(await _entitySet.ToListAsync());
        }

        public async Task<Result<List<TEntity>>> GetRangeAsync(int startIndex, int count)
        {
            var entities = await _entitySet.Skip(startIndex).Take(count).ToListAsync();

            return Result<List<TEntity>>.Success(entities);
        }

        public async Task<Result<List<TEntity>>> GetRangeAsync(ISpecification<TEntity> specification)
        {
            var entities = await _entitySet.Where(specification.ComposedCriteria).ToListAsync();
            return Result<List<TEntity>>.Success(entities);
        }

        public async Task<Result<List<TEntity>>> GetRangeAsync(ISpecification<TEntity> specification, int startIndex, int count)
        {
            var entities = await _entitySet.Where(specification.ComposedCriteria).Skip(startIndex).Take(count).ToListAsync();
            return Result<List<TEntity>>.Success(entities);
        }

        public async Task<Result> UpdateAsync(TEntity entity)
        {
            _entitySet.Update(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return Result.Fail(e.Message);
            }
            return Result.Success();
        }
    }
}
