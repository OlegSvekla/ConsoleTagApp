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

        public async Task<User> GetUserByIdAndDomainAsync(Guid userId, string domain)
        {
            var user = await _userRepository.GetOneByAsync(
                include: users => users.Include(user => user.UserTags).ThenInclude(ut => ut.Tag), 
                expression: user => user.Id.Equals(userId) && user.Domain.Equals(domain));

            return user; 
        } 

        public async Task<int> GetUsersCountByDomainAsync(string domain)
        {
            var totalUserCount = await _userRepository
                .GetManyByAsync(expression: user => user.Domain.Equals(domain))
                .CountAsync();

            return totalUserCount;
        }

        public async Task<List<User>> GetUsersByDomainAsync(string domain, int page, int pageSize)
        {
            var users = await _userRepository.GetManyByAsync(
                include: query => query.Include(user => user.UserTags).ThenInclude(ut => ut.Tag),
                expression: user => user.Domain.Equals(domain),
                cancellationToken: default)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return users;
        }

        public async Task<List<User>> GetUsersByTagAndDomainAsync(string tagValue, string domain)
        {            
            var users = await _userRepository.GetAllByAsync(
                include: query => query.Include(user => user.UserTags), 
                expression: user => user.Domain.Equals(domain) && user.UserTags
                .Any(tu => tu.Tag.Value.Equals(tagValue)));

            return users.ToList();
        }
    }
}
