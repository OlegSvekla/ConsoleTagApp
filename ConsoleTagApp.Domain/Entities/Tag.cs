using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTagApp.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Value { get; set; } = default!;
        public string Domain { get; set; } = default!;

        public ICollection<UserTag>? UserTags { get; set; }
    }
}
