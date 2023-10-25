using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTier.DataAccess.Configuration;
using ToDoAppNTier.Entities.Domains;

namespace ToDoAppNTier.DataAccess.Contexts
{
    public class ToDoContext : DbContext 
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) 
        { 

        }
        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkConfiguration());
        }
    }
}
