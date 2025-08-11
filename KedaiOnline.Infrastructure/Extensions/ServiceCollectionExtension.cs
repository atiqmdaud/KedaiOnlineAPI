using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using KedaiOnline.Infrastructure.Authorization;
using KedaiOnline.Infrastructure.Persistence;
using KedaiOnline.Infrastructure.Repositories;
using KedaiOnline.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KedaiOnline.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register your infrastructure services here
        // Example: services.AddScoped<IYourService, YourServiceImplementation>();

        // This is a placeholder for actual service registrations
        // services.AddScoped<IExampleService, ExampleService>();

        var connectionString = configuration.GetConnectionString("KedaiOnlineDb");
        services.AddDbContext<KedaiOnlineDbContext>(Options => 
        Options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<KedaiOnlineUserClaimsPrincipleFactory>()
            .AddEntityFrameworkStores<KedaiOnlineDbContext>();

        // Register the seeder
        services.AddScoped<IKedaiSeeder, KedaiSeeder>();//scope bucause it depends on DbContext
        services.AddScoped<IKedaiOnlineRepository, KedaiOnlineRepository>();
        services.AddScoped<IItemsRepository, ItemsRepository>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Malaysia"));
    }
}
