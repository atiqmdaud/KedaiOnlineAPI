using KedaiOnline.API.Middlewares;
using KedaiOnline.Application.Extensions;
using KedaiOnline.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

namespace KedaiOnline.API.Extensions;

public static class WebApplicationBuilderExt
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            //Array.Empty<string>()
            []
        }
    });

        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddScoped<ErrorHandlingMiddleware>();

        builder.Host.UseSerilog((context, configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
        );

    }
  
}
