using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.EntityConfigurations
{
    public class TeamLeadConfiguration : IEntityTypeConfiguration<TeamLead>
    {
        public void Configure(EntityTypeBuilder<TeamLead> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(TeamLead.Projects));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationTM = builder.Metadata.FindNavigation(nameof(TeamLead.TeamMembers));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(t => t.Lastname).IsRequired();
            builder.Property(t => t.Firstname).IsRequired();
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Password).IsRequired().HasMaxLength(8);

        }
    }
}
