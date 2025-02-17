using System.Linq.Expressions;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        public GenericRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }
        
        public IQueryable<T> GetAllQuery()
        {
            return  _db.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                _db.Set<T>().Add(entity);

                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> x,
            params Expression<Func<T, object>>[]? attributes)
        {
            IQueryable<T> results = _db.Set<T>().Where(x);

            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    results.Include(attribute);
                }
            }
            return await results.ToListAsync();

        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> x,
            params Expression<Func<T, object>>[]? attributes)
        {
            IQueryable<T> results = _db.Set<T>().Where(x);

            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                   
                    results = results.Include(attribute);

                }
            }
            return results;

        }
    
    }
}
