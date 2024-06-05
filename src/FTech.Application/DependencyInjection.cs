using FTech.Application.Mappers;
using FTech.Application.Services.Auth;
using FTech.Application.Services.Cars;
using FTech.Application.Services.Categories;
using FTech.Application.Services.Chats;
using FTech.Application.Services.Helpers;
using FTech.Application.Services.JWT;
using FTech.Application.Services.Messages;
using FTech.Infrastructure.Repositories.ChatUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTech.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IChatUserRepository, ChatUserRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddTransient<IJWTService, JWTService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}
