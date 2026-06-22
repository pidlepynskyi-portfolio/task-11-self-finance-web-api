using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using System.Linq.Expressions;

namespace ModelApi.Services.Repositories
{
    public class TypesIncomesRepository : BaseRepository, ITypesBaseRepository<TypeIncome>
    {
        public TypesIncomesRepository(SelfFinanceDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<TypeIncome>> GetAllAsync()
        {
            var result = await _dbContext.TypesIncomes.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TResult>> GetAllWithProjectionAsync<TResult>(Expression<Func<TypeIncome, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }


            var result = await _dbContext.TypesIncomes
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TypeIncome>> GetWithFilterByIdAsync(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesIncomes
                .Where(ti => ti.Id >= typeId)
                .ToListAsync();

            return result;
        }

        public async Task<TypeIncome> GetByIdAsync(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesIncomes
                .Where(ti => ti.Id == typeId)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task<TypeIncome> GetByIdWithDetailAsync(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesIncomes
                .Include(ti => ti.Incomes)
                .Where(ti => ti.Id == typeId)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task<TResult?> GetByIdWithProjectionAsync<TResult>(int? id, Expression<Func<TypeIncome, TResult>> selector)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.TypesIncomes
                .Where(ti => ti.Id == id)
                .Select(selector)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task CreateAsync(TypeIncome typesIncomes)
        {
            if (typesIncomes is null)
            {
                throw new ArgumentNullException(nameof(typesIncomes));
            }

            await _dbContext.TypesIncomes.AddAsync(typesIncomes);
        }

        public void Update(TypeIncome typesIncomes)
        {
            if (typesIncomes is null)
            {
                throw new ArgumentNullException(nameof(typesIncomes));
            }

            _dbContext.TypesIncomes.Update(typesIncomes);
        }

        public void Delete(TypeIncome typesIncomes)
        {
            if (typesIncomes is null)
            {
                throw new ArgumentNullException(nameof(typesIncomes));
            }

            if (typesIncomes.Incomes is null)
            {
                throw new NullReferenceException(nameof(typesIncomes));
            }

            if (typesIncomes.Incomes.Count > 0)
            {
                throw new InvalidOperationException("Deleting is not possible. \n\n" +
                    "Please clear the list Incomes fo this income type!");
            }

            _dbContext.TypesIncomes.Remove(typesIncomes);
        }

        public async Task<bool> IsFoundByFilterAsync(Expression<Func<TypeIncome, bool>> predicate)
        {
            var result = await _dbContext.TypesIncomes
                .Where(predicate)
                .FirstOrDefaultAsync();

            return result is null;
        }
    }
}
