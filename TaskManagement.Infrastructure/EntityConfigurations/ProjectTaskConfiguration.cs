using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Infrastructure.EntityConfigurations
{
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(ProjectTask.Todo));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(c => c.ProjectId).IsRequired();
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.TaskDescription).IsRequired();
            builder.Property(c => c.Priority).IsRequired();
            builder.Property(c => c.FromDate).IsRequired();
            builder.Property(c => c.ToDate).IsRequired();

        }
    }
}
