using ConsoleTagApp.Bl.Services;
using ConsoleTagApp.Domain.Entities;
using ConsoleTagApp.Domain.Interfaces.IRepositories;
using ConsoleTagApp.Domain.Interfaces.IServices;
using ConsoleTagApp.Infrastructure.Data.Repositories;
using ConsoleTagApp.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTagApp.Extensions
{
    public static class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMessageReader, MessageReader>()
                .AddScoped<IUserService<User>, UserService>()
                .AddDbContext<ConsoleDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("ConsoleTagAppDbConnection"));
                })
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
