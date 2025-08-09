using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using KedaiOnline.Infrastructure.Extensions;
using KedaiOnline.Infrastructure.Seeders;
using KedaiOnline.Application.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using KedaiOnline.API.Middlewares;
using KedaiOnline.Domain.Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddApplication();
//builder.Configuration.GetConnectionString("KedaiOnlineDb");
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Host.UseSerilog((context, configuration)=>
configuration
    .ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IKedaiSeeder>();
await seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>(); //1st middleware

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
