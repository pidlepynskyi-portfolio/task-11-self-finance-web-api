using System.Linq.Expressions;

namespace ModelApi.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetWithFilterByIdAsync(int? id);
        Task<T> GetByIdAsync(int? id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<IEnumerable<TResult>> GetAllWithProjectionAsync<TResult>(Expression<Func<T, TResult>> selector);
        Task<TResult?> GetByIdWithProjectionAsync<TResult>(int? id, Expression<Func<T, TResult>> selector);
    }
}
