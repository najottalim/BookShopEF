using System.Text;
using BookStore.Service.Interfaces;
using BookStore.Service.Mappers;
using BookStore.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BookStore.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddMyCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthManager, AuthManager>();
        services.AddAutoMapper(typeof(MapperProfile));   
    }
    
    public static string GetDbConnectionString(this IConfiguration configuration, string defaultConnection)
    {
        var postgresUrl = configuration.GetSection("DATABASE_URL").Value;
        if (string.IsNullOrEmpty(postgresUrl))
            return configuration.GetConnectionString(defaultConnection);

        var url = new Uri(postgresUrl, UriKind.Absolute);
        return $"Host={url.Host};User Id={url.UserInfo.Split(':')[0]};" +
               $"Password={url.UserInfo.Split(':')[1]};Database={url.LocalPath[1..]};";
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true, 
                ValidIssuer = config["Jwt:Issuer"],
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new
                    SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
            };
        });
    }
    
    public static void AddSwaggerAuthorization(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "BookStore.Api", Version = "v1"});

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

}