using System;
namespace TaskManagement.Web.VM
{
	public record ViewProject
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

