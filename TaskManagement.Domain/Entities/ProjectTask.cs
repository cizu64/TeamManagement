using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{

    public class ProjectTask:Entity
    {
        
        public ProjectTask(int teamLeadId,int projectId, string title,string taskDescription, string priority, string assignedTo, DateTime fromDate, DateTime toDate)
        {
            TeamLeadId = teamLeadId;
            ProjectId = projectId;
            Title = title;
            TaskDescription = taskDescription;
            Priority = priority;
            AssignedTo = assignedTo;
            FromDate = fromDate;
            ToDate = toDate;
        }
        public int TeamLeadId { get; private set; }
        public int ProjectId { get; private set; }
        public string Title { get; private set; }
        public string TaskDescription { get; private set; }
        public string Priority { get; private set; }
        public string AssignedTo { get; private set; } //assign to many team members
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsCompleted { get; private set; } = false;
        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;

        //child entity
        public Project Project { get; private set; }
        //navigational property
        private readonly List<Todo> todo = new();
        public IReadOnlyCollection<Todo> Todo => todo.AsReadOnly();

        //domain behavior
        public void AssignTeamMember(int teamMemberId)
        {
            AssignedTo += $"{teamMemberId}_"; //ids should be seperated with underscore
        }
    }
}
