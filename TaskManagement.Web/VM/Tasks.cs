namespace TaskManagement.Web.VM
{
    public class Tasks
    {
        public ProjectTask[] ProjectTasks { get; set; }
    }

    public class ProjectTask
    {
        public string name { get; set; }
        public string title { get; set; }
        public string taskDescription { get; set; }
        public string priority { get; set; }
        public bool status { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isCompleted { get; set; }
        public string assignedTo { get; set; }
        public DateTime dateCreated { get; set; }

    }
}

