using ConsoleTagApp.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleTagApp.Extensions
{
    public static class MigrationsConfiguration
    {
        public static async Task<IServiceProvider> RunDbContextMigrations(IServiceProvider app)
        {
            using (var scope = app.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<ConsoleDbContextSeed>>();

                logger.LogInformation("Database migration running...");

                try
                {
                    var context = serviceProvider.GetRequiredService<ConsoleDbContext>();
                    context.Database.Migrate();

                    await ConsoleDbContextSeed.SeedAsyncData(context, logger);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            return app;
        }
    }
}
