using ConsoleTagApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace ConsoleTagApp.Infrastructure.Data
{
    public class ConsoleDbContextSeed
    {
        public static async Task SeedAsyncData(ConsoleDbContext context/*, ILogger logger*/, int retry = 0)
        {
            var retryForAvailbility = retry;

            try
            {
                //logger.LogInformation("Data seeding started.");

                //if (!await context.Tags.AnyAsync())
                //{
                //    await context.Tags.AddRangeAsync(GetPreConfiguredTags());

                //    await context.SaveChangesAsync();
                //}
                if (!await context.Users.AnyAsync())
                {
                    await context.Users.AddRangeAsync(GetPreConfiguredUsers());

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailbility >= 10) throw;
                {
                    retryForAvailbility++;

                    //logger.LogError(ex.Message);
                    await SeedAsyncData(context, /*logger,*/ retryForAvailbility);
                }
                throw;
            }
        }

        private static IEnumerable<Tag> GetPreConfiguredTags()
        {
            var roles = new List<Tag>
            {

                new Tag { Value = "Art",        Domain = "Entertainment" },
                new Tag { Value = "Photography",Domain = "Entertainment" },
                new Tag { Value = "Fitness",    Domain = "Entertainment" },
                new Tag { Value = "Books",      Domain = "Entertainment" },

                new Tag { Value = "Science",    Domain = "Education" },
                new Tag { Value = "Book",       Domain = "Education" },

                new Tag { Value = "Programming",Domain = "Technology" },
                new Tag { Value = "Computer",   Domain = "Technology" },
                new Tag { Value = "C#",         Domain = "Technology" },

                new Tag { Value = "Football",   Domain = "Sports" },
                new Tag { Value = "Basketball", Domain = "Sports" },

                new Tag { Value = "Doctor",     Domain = "Health" },
                new Tag { Value = "Pill",       Domain = "Health" },

                new Tag { Value = "Tree ",      Domain = "Nature" },
                new Tag { Value = "Flower ",    Domain = "Nature" },

                new Tag { Value = "Cooking",    Domain = "Food" },
            };
            return roles;
        }

        private static IEnumerable<User> GetPreConfiguredUsers()
        {
            var tags = GetPreConfiguredTags().ToList();

            var users = new List<User>
                {
                    new User
                    {
                        Name = "User1",
                        Domain = "Entertainment",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[0].Id },
                            new UserTag { TagId = tags[1].Id },
                            new UserTag { TagId = tags[2].Id },
                            new UserTag { TagId = tags[3].Id }
                        },
                    },
                    new User
                    {
                        Name = "User2",
                        Domain = "Education",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[4].Id },
                            new UserTag { TagId = tags[5].Id }
                        }
                    },
                    new User
                    {
                        Name = "User3",
                        Domain = "Technology",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[6].Id },
                            new UserTag { TagId = tags[7].Id },
                            new UserTag { TagId = tags[8].Id }
                        }
                    },
                    new User
                    {
                        Name = "User4",
                        Domain = "Sports",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[9].Id },
                            new UserTag { TagId = tags[10].Id }
                        }
                    },
                    new User
                    {
                        Name = "User5",
                        Domain = "Health",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[11].Id },
                            new UserTag { TagId = tags[12].Id }
                        }
                    },
                    new User
                    {
                        Name = "User6",
                        Domain = "Nature",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[13].Id },
                            new UserTag { TagId = tags[14].Id }
                        }
                    },
                    new User
                    {
                        Name = "User7",
                        Domain = "Food",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[15].Id }
                        }
                    },
                    new User
                    {
                        Name = "User8",
                        Domain = "Sports",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[9].Id }
                        }
                    },
                    new User
                    {
                        Name = "User9",
                        Domain = "Sports",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[10].Id }
                        }
                    },
                    new User
                    {
                        Name = "User10",
                        Domain = "Health",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[11].Id },
                            new UserTag { TagId = tags[12].Id }
                        }
                    },
                    new User
                    {
                        Name = "User11",
                        Domain = "Entertainment",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[4].Id }
                        }
                    },
                    new User
                    {
                        Name = "User12",
                        Domain = "Food",
                        UserTags = new List<UserTag>
                        {
                            new UserTag { TagId = tags[15].Id }
                        }
                    },
                };
            return users;
        }
    }
}
