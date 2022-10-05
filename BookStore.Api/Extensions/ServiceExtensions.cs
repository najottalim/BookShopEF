using BookStore.Service.Interfaces;
using BookStore.Service.Mappers;
using BookStore.Service.Services;

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

    public static void AddJwtService(this IServiceCollection services)
    {
        
    }
}