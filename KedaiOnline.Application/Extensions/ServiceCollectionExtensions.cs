using KedaiOnline.Application.KedaiOnline;
using Microsoft.Extensions.DependencyInjection;

namespace KedaiOnline.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IKedaiOnlineService, KedaiOnlineService>();

        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

    }
}
