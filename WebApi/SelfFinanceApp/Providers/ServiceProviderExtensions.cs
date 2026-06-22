using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;
using InfrastructureApi.Services;
using InfrastructureApi.Common;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SelfFinanceApp.Services.RouteHistory;
using SelfFinanceApp.Services.RoutesCollection;
using SelfFinanceApp.Services.ViewModelServices;

namespace SelfFinanceApp.Providers
{
    public static class ServiceProviderExtensions
    {
        public static void AddSelfFinanceDbContext(this IServiceCollection services, string? connection)
        {
            services.AddDbContext<SelfFinanceDbContext>(options => options.UseSqlServer(connection));
        }

        public static void AddEfUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }

        public static void AddSelfFinanceEntityServices(this IServiceCollection services)
        {
            services.AddScoped<IEntityService<TypeIncomesDTO>, TypeIncomeService>();
            services.AddScoped<IEntityService<TypeExpensesDTO>, TypeExpenseService>();
            services.AddScoped<IEntityService<IncomeDTO>, IncomeService>();
            services.AddScoped<IEntityService<ExpenseDTO>, ExpenseService>();
            services.AddScoped<IBallanseService<IncomeDTO>, IncomeService>();
            services.AddScoped<IBallanseService<ExpenseDTO>, ExpenseService>();
        }

        public static void AddRouteHistoryService(this IServiceCollection services)
        {
            services.AddSingleton<RouteHistoryService>();
        }

        public static void AddRoutesApiCollectionService(this IServiceCollection services)
        {
            services.AddSingleton<RoutesCollectionService>();
        }

        public static void AddViewModelServices(this IServiceCollection services)
        {
            services.AddScoped<EntitiesService>();
            services.AddScoped<ReportService>();
        }
    }
}
