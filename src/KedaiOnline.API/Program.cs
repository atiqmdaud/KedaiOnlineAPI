using KedaiOnline.API.Extensions;
using KedaiOnline.API.Middlewares;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Infrastructure.Seeders;
using Serilog;

try
{
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

    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

} catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start correctly");
}
finally
{
    Log.CloseAndFlush();
}


public partial class Program { }
