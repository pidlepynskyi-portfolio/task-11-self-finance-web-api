using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;

namespace ModelApi.Services.DataSource
{
    public class SelfFinanceDbContext : DbContext
    {
        public virtual DbSet<TypeIncome> TypesIncomes { get; set; } = null!;
        public virtual DbSet<TypeExpense> TypesExpenses { get; set; } = null!;
        public virtual DbSet<Income> Incomes { get; set; } = null!;
        public virtual DbSet<Expense> Expenses { get; set; } = null!;

        public SelfFinanceDbContext(DbContextOptions<SelfFinanceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TypesIncomesConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TypesExpensesConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IncomesConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpensesConfiguration).Assembly);
        }
    }
}
