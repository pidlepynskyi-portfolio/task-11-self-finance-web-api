using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using System.Linq.Expressions;

namespace ModelApi.Services.Repositories
{
    public class TypesExpensesRepository : BaseRepository, ITypesBaseRepository<TypeExpense>
    {
        public TypesExpensesRepository(SelfFinanceDbContext dbContext) 
            : base(dbContext) { }

        public async Task<IEnumerable<TypeExpense>> GetAllAsync()
        {
            var result = await _dbContext.TypesExpenses.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TResult>> GetAllWithProjectionAsync<TResult>(Expression<Func<TypeExpense, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }


            var result = await _dbContext.TypesExpenses
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TypeExpense>> GetWithFilterByIdAsync(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesExpenses
                .Where(ti => ti.Id >= typeId)
                .ToListAsync();

            return result;
        }

        public async Task<TypeExpense> GetByIdAsync(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesExpenses
                .Where(ti => ti.Id == typeId)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task<TypeExpense> GetByIdWithDetailAsync(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesExpenses
                .Include(ti => ti.Expenses)
                .Where(ti => ti.Id == typeId)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task<TResult?> GetByIdWithProjectionAsync<TResult>(int? id, Expression<Func<TypeExpense, TResult>> selector)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.TypesExpenses
                .Where(ti => ti.Id == id)
                .Select(selector)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task CreateAsync(TypeExpense typesExpenses)
        {
            if (typesExpenses is null)
            {
                throw new ArgumentNullException(nameof(typesExpenses));
            }

            await _dbContext.TypesExpenses.AddAsync(typesExpenses);
        }

        public void Update(TypeExpense typesExpenses)
        {
            if (typesExpenses is null)
            {
                throw new ArgumentNullException(nameof(typesExpenses));
            }

            _dbContext.TypesExpenses.Update(typesExpenses);
        }

        public void Delete(TypeExpense typesExpenses)
        {
            if (typesExpenses is null)
            {
                throw new ArgumentNullException(nameof(typesExpenses));
            }

            _dbContext.TypesExpenses.Remove(typesExpenses);
        }

        public async Task<bool> IsFoundByFilterAsync(Expression<Func<TypeExpense, bool>> predicate)
        {
            var result = await _dbContext.TypesExpenses
                .Where(predicate)
                .FirstOrDefaultAsync();

            return result is null;
        }
    }
}
