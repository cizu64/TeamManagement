using System;
namespace TaskManagement.Web.VM
{
    public class TeamMemberVM
    {
        public TeamMembers[] TeamMembers { get; set; }
        public class TeamMembers
        {
            public int Id { get; set; }
            public string Firstname { get; private set; }
            public string Lastname { get; private set; }
        }
    }
}

