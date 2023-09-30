using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{
    public class Todo:Entity
    {
     
        public Todo(int teamMemberId, int projectTaskId, string title, string description)
        {
            TeamMemberId = teamMemberId;
            ProjectTaskId = projectTaskId;
            Title = title;
            Description = description;
        }

        public int ProjectTaskId { get; private set; }
        public int TeamMemberId { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; } = false;
        public DateTime DateCreated { get; private set; } = DateTime.Now;
    }
}
