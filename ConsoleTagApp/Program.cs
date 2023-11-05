using ConsoleTagApp.Domain.Interfaces.IRepositories;
using ConsoleTagApp.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

Console.WriteLine("Hello");