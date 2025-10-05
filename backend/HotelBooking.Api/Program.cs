using FluentValidation;
using HotelBooking.Api.Middleware;
using HotelBooking.Application.DependencyInjection;
using HotelBooking.Infrastructure.DependencyInjection;
using HotelBooking.Persistence;
using HotelBooking.Persistence.DependencyInjection;
using HotelBooking.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CurrentUser;
using SharedKernel.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Marketplace API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "'Bearer {your JWT token}'"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();


//only for one dbcontext
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<HotelBookingDbContext>();
await context.Database.MigrateAsync();
await DatabaseSeeder.SeedAsync(scope.ServiceProvider);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Marketplace API v1");
        options.RoutePrefix = "swagger";
        options.ConfigObject.AdditionalItems["persistAuthorization"] = true;
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
