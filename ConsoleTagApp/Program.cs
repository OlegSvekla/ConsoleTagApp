using ConsoleTagApp.Bl.Services;
using ConsoleTagApp.Domain.Interfaces.IRepositories;
using ConsoleTagApp.Domain.Interfaces.IServices;
using ConsoleTagApp.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

// Создайте объект ServiceProvider
var serviceProvider = new ServiceCollection()
    .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IMessageReader, MessageReader>()
    .BuildServiceProvider();

// Используйте ServiceProvider для получения зависимостей
var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
var messageReader = serviceProvider.GetRequiredService<IMessageReader>();

