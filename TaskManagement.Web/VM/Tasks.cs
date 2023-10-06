namespace TaskManagement.Web.VM
{
    public class Tasks
    {
        public IReadOnlyList<ProjectTask> ProjectTasks { get; set; }
    }

    public class ProjectTask
    {
        public int Name { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public string priority { get; set; }
        public string assignedTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool status { get; set; }
        public bool isCompleted { get; set; }
        public DateTime dateCreated { get; set; }
     
    }
}
