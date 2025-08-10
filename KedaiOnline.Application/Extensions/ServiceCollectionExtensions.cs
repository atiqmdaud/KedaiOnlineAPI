﻿using FluentValidation;
using FluentValidation.AspNetCore;
using KedaiOnline.Application.KedaiOnline;
using KedaiOnline.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace KedaiOnline.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();

    }
}
