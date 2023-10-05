using System;
using System.Collections.Generics;
namespace TaskManagement.Web.VM
{
	public class Projects
	{
        public IList<AllProject> Projects { get; set; }
        
    }
    public class AllProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

