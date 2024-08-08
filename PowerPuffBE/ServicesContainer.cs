namespace PowerPuffBE;

using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Service.Mappers;
using Service.Services;

public static class ServicesContainer
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IReactorRepository, ReactorRepository>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IReactorService, ReactorService>();
        services.AddScoped<IReactorMapper, ReactorMapper>();
        services.AddScoped<ISafetyStatusMapper, SafetyStatusMapper>();
    }

    public static void SeedDatabase(WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<PowerPuffDbContext>();
            context.Database.Migrate();
    
            if (!context.Reactors.Any())
            {
                var reactors = DataSeed.SeedReactors();
                context.Reactors.AddRange(reactors);
                context.SaveChanges();
            }

            if (!context.ReactorProductionChecks.Any())
            {
                var reactors = context.Reactors.ToList();
                var productionChecks = DataSeed.SeedProductionChecks(reactors);
                context.ReactorProductionChecks.AddRange(productionChecks);
                context.SaveChanges();
            }
        }
    }

}