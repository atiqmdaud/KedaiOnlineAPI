using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Interfaces;
using KedaiOnline.Domain.Repositories;
using KedaiOnline.Infrastructure.Authorization;
using KedaiOnline.Infrastructure.Authorization.Requirements;
using KedaiOnline.Infrastructure.Authorization.Services;
using KedaiOnline.Infrastructure.Configuration;
using KedaiOnline.Infrastructure.Persistence;
using KedaiOnline.Infrastructure.Repositories;
using KedaiOnline.Infrastructure.Seeders;
using KedaiOnline.Infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
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

        var connectionString = configuration.GetConnectionString("KedaiOnlineDb");
        services.AddDbContext<KedaiOnlineDbContext>(Options => 
        Options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<KedaiOnlineUserClaimsPrincipleFactory>()
            .AddEntityFrameworkStores<KedaiOnlineDbContext>();

        services.AddScoped<IKedaiSeeder, KedaiSeeder>();//scope because it depends on DbContext
        services.AddScoped<IKedaiOnlineRepository, KedaiOnlineRepository>();
        services.AddScoped<IItemsRepository, ItemsRepository>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality,
            builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Malaysia"))
            .AddPolicy(PolicyNames.AtLeast18,
            builder=>builder.AddRequirements(new MinimumAgeRequirement(18)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();

        services.AddScoped<IKedaiAuthorizationService, KedaiAuthorizationService>();

        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
