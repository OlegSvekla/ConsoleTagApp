using ConsoleTagApp.Bl.Services;
using ConsoleTagApp.Domain.Entities;
using ConsoleTagApp.Domain.Interfaces.IRepositories;
using ConsoleTagApp.Domain.Interfaces.IServices;
using ConsoleTagApp.Infrastructure.Data;
using ConsoleTagApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleTagApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMessageReader, MessageReader>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserService<User>, UserService>()
                .AddDbContext<ConsoleDbContext>(options =>
                {
                    options.UseSqlServer("ConsoleTagAppDbConnection");
                })
                .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService<User>>();
            var messageReader = scope.ServiceProvider.GetRequiredService<IMessageReader>();

            while (true)
            {
                Console.WriteLine("Select an action:");
                Console.WriteLine("1. Get user by Id and Domain");
                Console.WriteLine("2. Get users by Domain with pagination");
                Console.WriteLine("3. Get users by tag and Domain");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();
                if (choice == "0")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter user Id:");
                        if (Guid.TryParse(Console.ReadLine(), out Guid userId))
                        {
                            Console.WriteLine("Enter user Domain:");
                            string domainUser1 = Console.ReadLine();
                            var user = userService.GetUserByIdAndDomain(userId, domainUser1).Result;
                            if (user != null)
                            {
                                Console.WriteLine($"User Name: {user.Name}, Domain: {user.Domain}");
                            }
                            else
                            {
                                Console.WriteLine("User not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid user Id.");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Enter Domain:");
                        string domainUser2 = Console.ReadLine();
                        Console.WriteLine("Enter page:");
                        if (int.TryParse(Console.ReadLine(), out int page))
                        {
                            Console.WriteLine("Enter page size:");
                            if (int.TryParse(Console.ReadLine(), out int pageSize))
                            {
                                var users = userService.GetUsersByDomain(domainUser2, page, pageSize).Result;
                                if (users.Count > 0)
                                {
                                    foreach (var user in users)
                                    {
                                        Console.WriteLine($"User Name: {user.Name}, Domain: {user.Domain}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No users found for the specified domain and pagination.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid page size.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid page number.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Enter tag value:");
                        string tagValue = Console.ReadLine();
                        Console.WriteLine("Enter Domain:");
                        string domain = Console.ReadLine();
                        var usersByTag = userService.GetUsersByTagAndDomain(tagValue, domain).Result;
                        if (usersByTag.Count > 0)
                        {
                            foreach (var user in usersByTag)
                            {
                                Console.WriteLine($"User Name: {user.Name}, Domain: {user.Domain}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No users found for the specified tag and domain.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}