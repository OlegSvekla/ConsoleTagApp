using ConsoleTagApp.Domain.Entities;
using ConsoleTagApp.Domain.Interfaces.IRepositories;
using ConsoleTagApp.Infrastructure.Data;
using ConsoleTagApp.Infrastructure.Data.Repositories;

namespace ConsoleTagApp.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ConsoleDbContext dbContext) : base(dbContext)
        {
        }
    }
}
