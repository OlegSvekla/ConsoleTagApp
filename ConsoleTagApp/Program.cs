using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
