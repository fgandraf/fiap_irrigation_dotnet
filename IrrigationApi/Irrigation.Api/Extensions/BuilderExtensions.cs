using System.Text;
using System.Text.Json.Serialization;
using Irrigation.Api.Services;
using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Infra.Data;
using Irrigation.Infra.Repositories.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Irrigation.Api.Extensions;

public static class BuilderExtensions
{
    public static void AddConfigurationKeys(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString = builder.Configuration.GetConnectionString("Default");
        Configuration.Secrets.JwtPrivateKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;

    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IrrigationDataContext>(options =>
            options.UseSqlServer(
                Configuration.Database.ConnectionString,
                b => b.MigrationsAssembly("Irrigation.Api")
            )
        );
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }

    public static void AddAuthorizationPolicies(this WebApplicationBuilder builder)
        => builder.Services.AddAuthorization(x =>
        {
            x.AddPolicy("admin", p => p.RequireRole("admin"));
            x.AddPolicy("user", p => p.RequireRole("user"));
        });


    public static void AddRepositoryServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IrrigationDataContext>();
        
        builder.Services.AddScoped<IAreaRepository, AreaRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
        builder.Services.AddScoped<ISensorRepository, SensorRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
        
        builder.Services.AddScoped<ITokenService, TokenService>();
    }


    public static void AddSwaggerConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
        
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
        
            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        
        });
    }

    public static void JsonIgnoreCycles(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddJsonOptions(options => 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    }
}