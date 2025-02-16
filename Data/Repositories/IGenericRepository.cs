using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllQuery();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> x,
            params Expression<Func<T, object>>[] attributes);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> x,
            params Expression<Func<T, object>>[]? attributes);

    }
}
