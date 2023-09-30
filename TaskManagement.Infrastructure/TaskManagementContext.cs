using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Infrastructure
{
    public class TaskManagementContext : DbContext, IUnitOfWork
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options): base(options)
        {
            
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectTask> ProjectTask { get; set; }
        public DbSet<TeamLead> TeamLead { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
        public DbSet<Todo> Todo { get; set; }
        public DbSet<Log> Log { get; set; }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync();
            return true;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
