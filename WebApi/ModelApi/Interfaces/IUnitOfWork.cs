using ModelApi.Entities;

namespace ModelApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITypesBaseRepository<TypeIncome> TypesIncomes { get; }
        ITypesBaseRepository<TypeExpense> TypesExpenses { get; }
        IBallanseRepository<Income> Incomes { get; }
        IBallanseRepository<Expense> Expenses { get; }

        Task SaveAsync();
    }
}
