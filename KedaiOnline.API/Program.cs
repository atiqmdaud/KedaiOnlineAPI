using KedaiOnline.API.Extensions;
using KedaiOnline.API.Middlewares;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();

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
