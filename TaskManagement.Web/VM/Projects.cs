using System;
using System.Collections.Generic;
namespace TaskManagement.Web.VM
{
    public class Projects
    {
        public IReadOnlyList<AllProject> AllProjects { get; set; }

    }
    public class AllProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string AssignedTeamMemberIds { get; set; }
        public bool IsActive { get; set; }
    }
}

