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
        Task<T> GetUserByIdAndDomain(Guid userId, string domain);
        Task<List<T>> GetUsersByDomain(string domain, int page, int pageSize);
        Task<List<T>> GetUsersByTagAndDomain(string tagValue, string domain);
    }
}