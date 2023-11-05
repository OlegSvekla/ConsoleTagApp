using ConsoleTagApp.Domain.Entities;

namespace ConsoleTagApp.Domain.PaginationEntities
{
    public class PagedUserAndTagsResult
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
