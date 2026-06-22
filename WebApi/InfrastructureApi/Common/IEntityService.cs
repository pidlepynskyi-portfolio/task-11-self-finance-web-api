namespace InfrastructureApi.Common
{
    public interface IEntityService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int? id);
        Task CreateAsync(T entity);
        Task UpdateAsync(int? id, T entity);
        Task DeleteAsync(int? id);
    }
}
