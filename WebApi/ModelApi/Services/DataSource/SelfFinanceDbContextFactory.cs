using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ModelApi.Services.DataSource
{
    public class SelfFinanceDbContextFactory : IDesignTimeDbContextFactory<SelfFinanceDbContext>
    {
        public SelfFinanceDbContext CreateDbContext(string[] args)
        {
            //ConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.AddJsonFile("appsettings.json");
            //IConfigurationRoot config = builder.Build();


            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            return new SelfFinanceDbContext(optionsBuilder.Options);
        }
    }
}
