using ConsoleTagApp.Bl.Services;
using ConsoleTagApp.Domain.Entities;
using ConsoleTagApp.Domain.Interfaces.IRepositories;
using ConsoleTagApp.Domain.Interfaces.IServices;
using ConsoleTagApp.Extensions;
using ConsoleTagApp.Infrastructure.Data;
using ConsoleTagApp.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleTagApp
{
    class Program
    {
        //public class ConsoleDbContextFactory : IDesignTimeDbContextFactory<ConsoleDbContext>
        //{
        //    public ConsoleDbContext CreateDbContext(string[] args)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json")
        //            .Build();

        //        var optionsBuilder = new DbContextOptionsBuilder<ConsoleDbContext>();
        //        var connectionString = configuration.GetConnectionString("ConsoleTagAppDbConnection");
        //        optionsBuilder.UseSqlServer(connectionString);

        //        return new ConsoleDbContext(optionsBuilder.Options);
        //    }
        //}

        static void Main(string[] args)
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
                .AddLogging(builder =>
                {
                    builder.AddConsole(); // или другой поставщик журналов
                })
                .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService<User>>();
            var messageReader = scope.ServiceProvider.GetRequiredService<IMessageReader>();
            //var context = scope.ServiceProvider.GetRequiredService<ConsoleDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            MigrationsConfiguration.RunDbContextMigrations(serviceProvider/*, logger*/);

            while (true)
            {
                //Console.WriteLine("Select an action:");
                //Console.WriteLine("1. Get user by Id and Domain");
                //Console.WriteLine("2. Get users by Domain with pagination");
                //Console.WriteLine("3. Get users by tag and Domain");
                //Console.WriteLine("0. Exit");

                Console.WriteLine("Select an action:");
                Console.WriteLine("1. Read messages from a file");
                Console.WriteLine("2. Get user by Id and Domain");
                Console.WriteLine("3. Get users by Domain with pagination");
                Console.WriteLine("4. Get users by tag and Domain");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();
                if (choice == "0")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Drop a text file here and press Enter:");

                        // Пользователь сбрасывает файл в консоль
                        string filePath = Console.ReadLine();

                        if (File.Exists(filePath))
                        {
                            using (FileStream fileStream = File.OpenRead(filePath))
                            {
                                while (true)
                                {
                                    // Здесь вы можете использовать messageReader для чтения сообщений из файла
                                    var message = messageReader.ReadMessage(fileStream, (byte)'\n' );
                                    if (message == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        //foreach (var item in message)
                                        //{
                                        //    Console.WriteLine(item);
                                        //}
                                        Console.WriteLine(message);
                                    }

                                    // Остальной код для обработки сообщения
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("File not found. Please make sure the file exists and try again.");
                        }

                        break;
                    case "2":
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
                    case "3":
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
                    case "4":
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