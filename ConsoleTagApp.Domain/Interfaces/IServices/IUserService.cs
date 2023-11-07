using ConsoleTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTagApp.Domain.Interfaces.IServices
{
    public interface IUserService<T> where T : class
    {
        Task<T> GetUserByIdAndDomainAsync(Guid userId, string domain);
        Task<List<T>> GetUsersByDomainAsync(string domain, int page, int pageSize);
        Task<List<T>> GetUsersByTagAndDomainAsync(string tagValue, string domain);
        Task<int> GetUsersCountByDomainAsync(string domain);
    }
}