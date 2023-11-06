using ConsoleTagApp.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
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

                try
                {
                    var context = serviceProvider.GetRequiredService<ConsoleDbContext>();
                    context.Database.Migrate();

                    await ConsoleDbContextSeed.SeedAsyncData(context);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while seeding the database.");
                }
            }
            return app;
        }
    }
}
