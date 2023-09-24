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
        public enum PRIORITY
        {
            High,
            Medium,
            Low
        }
        public ProjectTask(int projectId, int taskDescription, PRIORITY priority, string[] assignedTo, DateTime fromDate, DateTime toDate, bool isCompleted)
        {
            ProjectId = projectId;
            TaskDescription = taskDescription;
            Priority = nameof(priority);
            AssignedTo = assignedTo;
            FromDate = fromDate;
            ToDate = toDate;
            IsCompleted = isCompleted;
        }

        public int ProjectId { get; private set; }
        public int TaskDescription { get; private set; }
        public string Priority { get; private set; }
        public string[] AssignedTo { get; private set; }
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsCompleted { get; private set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;
    }
}
