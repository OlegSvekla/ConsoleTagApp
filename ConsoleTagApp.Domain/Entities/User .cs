using System.ComponentModel.DataAnnotations;

namespace ConsoleTagApp.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Domain { get; set; } = default!;

        public List<TagToUser>? TagsToUser { get; set; }
    }
}