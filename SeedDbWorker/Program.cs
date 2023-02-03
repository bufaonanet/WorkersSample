using Microsoft.EntityFrameworkCore;
using SeedDbWorker;
using SeedDbWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        AppSettings.Configuration = configuration;
        AppSettings.ConnectionString = configuration.GetConnectionString("DefaultConnection");

        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionBuilder.UseSqlServer(AppSettings.ConnectionString);

        services.AddScoped<AppDbContext>(d => new AppDbContext(optionBuilder.Options));   

        services.AddHostedService<Worker>();
    })
    .Build();

CreateDatabaseIfNotExist(host);

await host.RunAsync();


void CreateDatabaseIfNotExist(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception)
        {
            throw;
        }

    }
}
