using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using System.Linq.Expressions;

namespace ModelApi.Services.Repositories
{
    public class IncomeRepository : BaseRepository, IBallanseRepository<Income>
    {
        public IncomeRepository(SelfFinanceDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<Income>> GetAllAsync()
        {
            var result = await _dbContext.Incomes.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TResult>> GetAllWithProjectionAsync<TResult>(Expression<Func<Income, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }


            var result = await _dbContext.Incomes
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Income>> GetWithFilterByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var result = await _dbContext.Incomes
                .Where(ti => ti.Id >= id)
                .ToListAsync();

            return result;
        }

        public async Task<Income> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var result = await _dbContext.Incomes
                .Where(ti => ti.Id == id)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task<TResult?> GetByIdWithProjectionAsync<TResult>(int? id, Expression<Func<Income, TResult>> selector)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.Incomes
                .Where(ti => ti.Id == id)
                .Select(selector)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<double?> GetSumByEnterDateAsync(DateTime toDate)
        {
            var result = await _dbContext.Incomes
                .Where(IsByEnteredDate(toDate))
                .SumAsync(i => i.Amount.Value);

            return result;
        }

        public async Task<IEnumerable<TResult>> GetByEnterDateWithDetailAndProjectionAsync<TResult>(DateTime toDate, Expression<Func<Income, TResult>> selector)
        {
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.Incomes
                .Include(i => i.TypeIncome)
                .Where(IsByEnteredDate(toDate))
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<double?> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await _dbContext.Incomes
                .Where(IsByPeriod(fromDate, toDate))
                .SumAsync(i => i.Amount.Value);

            return result;
        }

        public async Task<IEnumerable<TResult>> GetByPeriodWithDetailAndProjectionAsync<TResult>(DateTime fromDate, DateTime toDate, Expression<Func<Income, TResult>> selector)
        {
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.Incomes
                .Include(ti => ti.TypeIncome)
                .Where(IsByPeriod(fromDate, toDate))
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task CreateAsync(Income income)
        {
            if (income is null)
            {
                throw new ArgumentNullException(nameof(income));
            }

            await _dbContext.Incomes.AddAsync(income);
        }

        public void Update(Income income)
        {
            if (income is null)
            {
                throw new ArgumentNullException(nameof(income));
            }

            _dbContext.Incomes.Update(income);
        }

        public void Delete(Income income)
        {
            if (income is null)
            {
                throw new ArgumentNullException(nameof(income));
            }

            _dbContext.Incomes.Remove(income);
        }

        private Expression<Func<Income, bool>> IsByEnteredDate(DateTime toDate)
        {
            return i => i.CreateDate.Value.Date == toDate.Date;
        }

        private Expression<Func<Income, bool>> IsByPeriod(DateTime fromDate, DateTime toDate)
        {
            return i => i.CreateDate.Value.Date >= fromDate.Date
                && i.CreateDate.Value.Date <= toDate.Date;
        }
    }
}
