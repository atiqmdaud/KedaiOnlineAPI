using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using KedaiOnline.Infrastructure.Extensions;
using KedaiOnline.Infrastructure.Seeders;
using KedaiOnline.Application.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddApplication();
//builder.Configuration.GetConnectionString("KedaiOnlineDb");
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration)=>
configuration
    .MinimumLevel.Override("microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("microsoft.EntityFrameworkCore", LogEventLevel.Information)
    .WriteTo.Console(outputTemplate:"[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}]")
);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IKedaiSeeder>();
await seeder.Seed();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
