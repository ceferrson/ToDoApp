using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppNTier.Dtos.Dtos
{
    public class WorkCreateDto
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
