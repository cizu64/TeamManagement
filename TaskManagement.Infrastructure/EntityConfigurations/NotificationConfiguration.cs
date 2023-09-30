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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Role).IsRequired();
        }
    }
}
