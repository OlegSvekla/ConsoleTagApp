using ConsoleTagApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


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
                    throw ex;
                }
            }
            return app;
        }
    }
}
