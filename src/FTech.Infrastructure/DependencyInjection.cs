using FTech.Infrastructure.Data;
using FTech.Infrastructure.Repositories.Cars;
using FTech.Infrastructure.Repositories.Categories;
using FTech.Infrastructure.Repositories.Chats;
using FTech.Infrastructure.Repositories.ChatUsers;
using FTech.Infrastructure.Repositories.Drivers;
using FTech.Infrastructure.Repositories.Messages;
using FTech.Infrastructure.Repositories.Users;
using FTech.Infrastructure.Services.Files;
using FTech.Infrastructure.Services.OTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace FTech.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("postgresql")));

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDriverRepostory, DriverRepository>();

            //SMS
            services.AddScoped<ISMSService, SMSService>();

            //File
            services.AddScoped<IFileService, FileService>();

            //Chat
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatUserRepository, ChatUserRepository>();

            //Car
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    });

            return services;
        }
    }
}