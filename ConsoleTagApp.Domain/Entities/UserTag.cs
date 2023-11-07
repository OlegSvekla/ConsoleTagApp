using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTagApp.Domain.Entities
{
    public class UserTag
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
