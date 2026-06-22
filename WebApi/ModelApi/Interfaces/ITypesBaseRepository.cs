using ModelApi.Entities;
using System.Linq.Expressions;

namespace ModelApi.Interfaces
{
    public interface ITypesBaseRepository<T> : IRepository<T> where T : class
    {
        Task<T> GetByIdWithDetailAsync(int? id);
        Task<bool> IsFoundByFilterAsync(Expression<Func<T, bool>> predicate);
    }
}
