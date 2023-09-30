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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Project.ProjectTasks));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.AssignedTeamMemberIds).IsRequired();
            builder.Property(c => c.TeamLeadId).IsRequired();

        }
    }
}
