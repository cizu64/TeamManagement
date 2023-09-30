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
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(TeamMember.Todo));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(t => t.CountryId).IsRequired();
            builder.Property(t => t.TeamLeadId).IsRequired();
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Firstname).IsRequired();
            builder.Property(t => t.Lastname).IsRequired();
            builder.Property(t => t.Password).IsRequired().HasMaxLength(8);

        }
    }
}
