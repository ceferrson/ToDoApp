using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTier.Entities.Domains;

namespace ToDoAppNTier.DataAccess.Configuration
{
    public class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.Property(w => w.Title).HasMaxLength(100).IsRequired();
            builder.Property(w => w.IsCompleted).IsRequired();
        }
    }
}
