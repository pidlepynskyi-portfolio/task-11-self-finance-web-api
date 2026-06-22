using System.Linq.Expressions;

namespace ModelApi.Interfaces
{
    public interface IBallanseRepository<T> : IRepository<T> where T : class
    {
        Task<double?> GetSumByEnterDateAsync(DateTime toDate);
        Task<double?> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<TResult>> GetByEnterDateWithDetailAndProjectionAsync<TResult>(DateTime toDate, Expression<Func<T, TResult>> selector);
        Task<IEnumerable<TResult>> GetByPeriodWithDetailAndProjectionAsync<TResult>(DateTime fromDate, DateTime toDate, Expression<Func<T, TResult>> selector);
    }
}
