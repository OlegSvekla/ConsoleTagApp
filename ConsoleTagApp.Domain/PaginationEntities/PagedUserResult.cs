using ConsoleTagApp.Domain.Entities;

namespace ConsoleTagApp.Domain.PaginationEntities
{
    public class PagedUserResult
    {
        public IEnumerable<User> Users { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
