using System.Security.Claims;
using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFastEndpoints();

builder.Services.AddJwtBearerServices(builder.Configuration); // Extension method from Extensions>ServiceRegistration.cs
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerDoc(); //add this

builder.Services.AddRepositoryServices();

builder.Services.AddDbContextServices(builder.Configuration); // Extension method

builder.Services.AddUtilityServices();

builder.Services.AddBusinessRuleServices();


builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseFastEndpoints(c => {
    c.Endpoints.RoutePrefix = "api";
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
app.UseOpenApi();

app.UseSwaggerUi3(s => s.ConfigureDefaults()); // Method from FastEndpoints.Swagger


app.Run();
