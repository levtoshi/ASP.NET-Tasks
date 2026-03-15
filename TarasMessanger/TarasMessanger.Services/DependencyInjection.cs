using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.Services;
using TarasMessanger.Storage;
using TarasMessanger.Storage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Services.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerConnectionString = configuration.GetConnectionString("SqlServer") ??
                throw new Exception("Connection string `SqlServer` not found.");

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(sqlServerConnectionString);
            });

            return services;
        }

        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<MessageService>();

            services.AddScoped<IPrivateChatRepository, PrivateChatRepository>();
            services.AddScoped<PrivateChatService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<PostService>();

            return services;
        }
    }
}
