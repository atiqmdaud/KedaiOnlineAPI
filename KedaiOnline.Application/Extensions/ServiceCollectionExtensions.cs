using FluentValidation;
using FluentValidation.AspNetCore;
using KedaiOnline.Application.KedaiOnline;
using Microsoft.Extensions.DependencyInjection;

namespace KedaiOnline.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IKedaiOnlineService, KedaiOnlineService>();

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

    }
}
