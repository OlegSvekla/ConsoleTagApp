using ConsoleTagApp.Domain.Entities;
using ConsoleTagApp.Domain.Interfaces.IServices;
using ConsoleTagApp.Extensions;
using ConsoleTagApp.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleTagApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = ServiceConfiguration.ConfigureServices();
            await MigrationsConfiguration.RunDbContextMigrations(serviceProvider);

            using var scope = serviceProvider.CreateScope();

            var userService = scope.ServiceProvider.GetRequiredService<IUserService<User>>();
            var messageReader = scope.ServiceProvider.GetRequiredService<IMessageReader>();
            var context = scope.ServiceProvider.GetRequiredService<ConsoleDbContext>();



            while (true)
            {
                Console.WriteLine("Select an action:");
                Console.WriteLine("1. Read messages from a file");
                Console.WriteLine("2. Get user with Tags by Id and Domain");
                Console.WriteLine("3. Get users with Tags by Domain. Pagination used");
                Console.WriteLine("4. Get users by Tag and Domain");
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

                        string filePath = Console.ReadLine();
                        if (File.Exists(filePath))
                        {
                            using (FileStream fileStream = File.OpenRead(filePath))
                            {
                                int number = 0;
                                while (true)
                                {
                                    var message = messageReader.ReadMessage(fileStream, (byte)'\n' );
                                    if (message is null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        number++;
                                        Console.WriteLine($"Here is your message {number}: {message}");
                                    }
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
                            string Domain = Console.ReadLine();

                            var user = userService.GetUserByIdAndDomainAsync(userId, Domain).Result;
                            if (user is not null)
                            {
                                Console.WriteLine($"User Name: {user.Name}, Domain: {user.Domain}");
                                if (user.UserTags is not null && user.UserTags.Any())
                                {
                                    Console.WriteLine("Tags:");

                                    foreach (var userTag in user.UserTags)
                                    {
                                        if (userTag.Tag is not null)
                                        {
                                            Console.WriteLine($"- Tag Value: {userTag.Tag.Value}");
                                        }
                                    }
                                }
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
                        string userDomain = Console.ReadLine();

                        var totalUsersCount = await userService.GetUsersCountByDomainAsync(userDomain);
                        if (totalUsersCount is 0)
                        {
                            Console.WriteLine("No users found for the specified domain.");
                        }
                        else
                        {
                            Console.WriteLine($"Found {totalUsersCount} user(s) for the specified domain.");

                            int maxPageSize = Math.Min(4, totalUsersCount);
                            int userChoice = 0;

                            if (totalUsersCount > 1)
                            {
                                Console.WriteLine($"How many users would you like to see on one page? Enter a number between 1 and {maxPageSize}: ");
                                while (userChoice < 1 || userChoice > maxPageSize)
                                {
                                    if (int.TryParse(Console.ReadLine(), out userChoice) && userChoice >= 1 && userChoice <= maxPageSize)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid page size.");
                                    }
                                }
                            }
                             int pageCount = (int)Math.Ceiling((double)totalUsersCount / userChoice);
                             Console.WriteLine($"You will have {pageCount} page(s).");

                             int pageChoice = 0;

                             Console.WriteLine($"Which page would you like to see? Enter a number between 1 and {pageCount}: ");
                             while (pageChoice < 1 || pageChoice > pageCount)
                             {
                                 if (int.TryParse(Console.ReadLine(), out pageChoice) && pageChoice >= 1 && pageChoice <= pageCount)
                                 {
                                     break;
                                 }
                                 else
                                 {
                                     Console.WriteLine("Invalid input. Please enter a valid page number.");
                                 }
                             }
                             var users = userService.GetUsersByDomainAsync(userDomain, pageChoice, userChoice).Result;

                             Console.WriteLine($"Page {pageChoice}:");
                             foreach (var user in users)
                             {
                                 Console.WriteLine($"User Name: {user.Name}, Domain: {user.Domain}");
                                 if (user.UserTags is not null && user.UserTags.Any())
                                 {
                                     Console.WriteLine("Tags:");
                                     foreach (var userTag in user.UserTags)
                                     {
                                         if (userTag.Tag is not null)
                                         {
                                             Console.WriteLine($"- Tag Value: {userTag.Tag.Value}");
                                         }
                                     }
                                 }
                             }
                        }
                    break;
                    case "4":
                        Console.WriteLine("Enter tag value:");
                        string tagValue = Console.ReadLine();

                        Console.WriteLine("Enter Domain:");
                        string domain = Console.ReadLine();

                        var usersByTagAndDomain = userService.GetUsersByTagAndDomainAsync(tagValue, domain).Result;
                        if (usersByTagAndDomain.Count > 0)
                        {
                            foreach (var user in usersByTagAndDomain)
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