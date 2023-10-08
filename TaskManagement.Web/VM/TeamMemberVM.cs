using System;
namespace TaskManagement.Web.VM
{
    public class TeamMemberVM
    {
        public TeamMembers[] Members { get; set; }
        public class TeamMembers
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
        }
    }
}

