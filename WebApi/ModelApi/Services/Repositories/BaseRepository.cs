using ModelApi.Services.DataSource;

namespace ModelApi.Services.Repositories
{
    public abstract class BaseRepository 
    {
        protected readonly SelfFinanceDbContext _dbContext;

        public BaseRepository(SelfFinanceDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
