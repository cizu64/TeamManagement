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
        public Project(int name, int teamLeadId, bool isActive, string[] assignedTeamMemberIds)
        {
            Name = name;
            TeamLeadId = teamLeadId;
            AssignedTeamMemberIds = assignedTeamMemberIds;
        }
        public int Name { get; private set; }

        public int TeamLeadId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime DateCreated { get; private set; } = DateTime.Now;
        public string[] AssignedTeamMemberIds { get; set; }
    }
}
