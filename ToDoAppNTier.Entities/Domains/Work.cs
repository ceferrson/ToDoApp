using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppNTier.Entities.Domains
{
    public class Work : BaseEntity
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
