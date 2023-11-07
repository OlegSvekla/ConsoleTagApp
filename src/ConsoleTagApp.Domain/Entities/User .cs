using System.ComponentModel.DataAnnotations;

namespace ConsoleTagApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Domain { get; set; } = default!;

        public ICollection<UserTag>? UserTags { get; set; }
    }
}