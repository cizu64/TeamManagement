using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{
    public class Project:Entity
    {
        public Project(string name, int teamLeadId, string description, string assignedTeamMemberIds)
        {
            Name = name;
            TeamLeadId = teamLeadId;
            Description = description;
            AssignedTeamMemberIds = assignedTeamMemberIds;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TeamLeadId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;
        public string AssignedTeamMemberIds { get; private set; } 

        public TeamLead TeamLead { get; private set; }

        //navigational property for ProjectTask because there is a one-to-many relationship between Project and ProjectTask
        private readonly List<ProjectTask> projectTasks = new();
        public IReadOnlyCollection<ProjectTask> ProjectTasks => projectTasks.AsReadOnly();

        //behavior to assign team member to project
        public void AssignTeamMember(int teamMemberId)
        {
            AssignedTeamMemberIds += $"{teamMemberId}_"; //ids should be seperated with underscore
        }
    }
}
