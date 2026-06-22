using Microsoft.EntityFrameworkCore;
using SelfFinanceApp.Services.Endpoints;
using SelfFinanceApp.Providers;
using Serilog;
using SelfFinanceApp.Components;
using MudBlazor.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services));

    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
    builder.Services.AddHttpClient();

    builder.Services.AddSelfFinanceDbContext(connection);
    builder.Services.AddEfUnitOfWorkService();
    builder.Services.AddSelfFinanceEntityServices();
    builder.Services.AddRouteHistoryService();
    builder.Services.AddRoutesApiCollectionService();
    builder.Services.AddViewModelServices();
    builder.Services.AddMudServices();

    var app = builder.Build();

    var apiService = new ApiService(app);

    app.UseStaticFiles();
    app.UseSerilogRequestLogging();

    
    app.UseAntiforgery();

    app.UseExceptionHandler(app => app.Run(async context =>
    {
        await context.Response.WriteAsJsonAsync(new { code = context.Response.StatusCode, message = "Unexpected error!\nPlease contact the site administrator to clarify the problem." });
    }));

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    apiService.MapApi();

    await app.RunAsync();

    Log.Information("Stopped cleanly");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}