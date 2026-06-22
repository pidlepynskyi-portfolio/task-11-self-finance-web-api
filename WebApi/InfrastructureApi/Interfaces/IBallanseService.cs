namespace InfrastructureApi.Interfaces
{
    public interface IBallanseService<T>
    {
        Task<double> GetSumByEnterDateAsync(DateTime toDate);
        Task<double> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<T>> GetByEnterDateAsync(DateTime toDate);
        Task<IEnumerable<T>> GetByPeriodAsync(DateTime fromDate, DateTime toDate);
    }
}
