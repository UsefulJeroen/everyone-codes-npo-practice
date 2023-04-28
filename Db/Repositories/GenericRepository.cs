using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        public Task<bool> AnyAsync();
        public Task AddAsync(T t);
        public Task AddAsync(List<T> t);
        public Task<int> CountAsync();
        public Task<int> CountAsync(Expression<Func<T, bool>> match);
        public Task DeleteAsync(Expression<Func<T, bool>> match);
        public Task<T?> FindAsync(Expression<Func<T, bool>> match);
        public Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        public Task<ICollection<T>> GetAllAsync();
        public Task<T?> GetAsync(int id);
        public Task<T?> UpdateAsync(T t, int id);
    }

    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly ApiDbContext _dbContext;
        protected readonly ILogger<GenericRepository<T>> _logger;

        protected GenericRepository(ApiDbContext dbContext,
                                    ILogger<GenericRepository<T>> logger
            )
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public virtual async Task<bool> AnyAsync()
        {
            return await _dbContext.Set<T>().AnyAsync();
        }

        public virtual async Task AddAsync(T t)
        {
            await _dbContext.Set<T>().AddAsync(t);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task AddAsync(List<T> t)
        {
            await _dbContext.Set<T>().AddRangeAsync(t);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().AsNoTracking().CountAsync(match);
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> match)
        {
            try
            {
                var set = _dbContext.Set<T>();
                var old = set.Where(match);
                set.RemoveRange(old);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
            }
        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> UpdateAsync(T t, int id)
        {
            if (t == null)
            {
                return null;
            }
            var exists = await _dbContext.Set<T>().FindAsync(id);
            if (exists == null)
            {
                return null;
            }
            _dbContext.Entry(exists).CurrentValues.SetValues(t);

            await _dbContext.SaveChangesAsync();

            return exists;
        }
    }
}
