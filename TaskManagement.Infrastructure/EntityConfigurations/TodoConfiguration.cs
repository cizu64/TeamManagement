using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.EntityConfigurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {           
            builder.Property(t => t.TeamMemberId).IsRequired();
            builder.Property(t => t.ProjectTaskId).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Description).IsRequired();
        }
    }
}
