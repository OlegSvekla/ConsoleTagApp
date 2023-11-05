using ConsoleTagApp.Domain.Entities;
using ConsoleTagApp.Domain.Interfaces.IRepositories;
using ConsoleTagApp.Domain.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTagApp.Bl.Services
{
    public class UserService : IUserService<User>
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<User> GetUserByIdAndDomain(Guid userId, string domain)
        {
            Expression<Func<User, bool>> filter = user => user.Id == userId && user.Domain == domain;

            return await _userRepository.GetOneByAsync(
                include: users => users.Include(user => user.UserTags), expression: filter);//?
        }

        public async Task<List<User>> GetUsersByDomain(string domain, int page, int pageSize)
        {

            return await _userRepository.GetOneByAsyncWithPagin(
                include: query => query.Include(user => user.UserTags),
                expression: user => user.Domain == domain,
                cancellationToken: default)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersByTagAndDomain(string tagValue, string domain)
        {
            Expression<Func<User, bool>> filter = user =>
                user.Domain == domain &&
                user.UserTags.Any(tu => tu.Tag.Value == tagValue);

            var usersQuery = await _userRepository.GetAllByAsync(include: query => query.Include(user => user.UserTags), expression: filter);

            return usersQuery.ToList();
        }
    }
}
