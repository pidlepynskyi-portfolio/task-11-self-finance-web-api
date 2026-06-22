using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using ModelApi.Services.Repositories;

namespace ModelApi.Services.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SelfFinanceDbContext _dbContext;
        private TypesIncomesRepository _typesIncomesRepository;
        private TypesExpensesRepository _typesExpensesRepository;
        private IncomeRepository _incomeRepository;
        private ExpenseRepository _expenseRepository;
        private bool _disposed = false;

        public EFUnitOfWork(SelfFinanceDbContext dbContext)
        {
            _dbContext = dbContext;

            _typesIncomesRepository = new TypesIncomesRepository(dbContext);
            _typesExpensesRepository = new TypesExpensesRepository(dbContext);
            _incomeRepository = new IncomeRepository(dbContext);
            _expenseRepository = new ExpenseRepository(dbContext);
        }

        public ITypesBaseRepository<TypeIncome> TypesIncomes => _typesIncomesRepository;
        public ITypesBaseRepository<TypeExpense> TypesExpenses => _typesExpensesRepository;
        public IBallanseRepository<Income> Incomes => _incomeRepository;
        public IBallanseRepository<Expense> Expenses => _expenseRepository;

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
