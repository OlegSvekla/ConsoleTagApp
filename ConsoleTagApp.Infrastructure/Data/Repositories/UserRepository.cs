using SimpleChat.Core.Entities;
using SimpleChat.Core.Interfaces.IRepositories;

namespace SimpleChat.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SimpleChatDbContext dbContext) : base(dbContext)
        {
        }
    }
}
