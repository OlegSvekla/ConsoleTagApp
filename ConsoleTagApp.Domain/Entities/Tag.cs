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
        [Required]
        public string Value { get; set; } = default!;

        [Required]
        public string Domain { get; set; } = default!;

        public List<User>? Users { get; set; }
    }
}
