namespace TaskManagement.WebAPI.DTO
{
    public enum Priority
    {
        HIGH,
        MEDIUM,
        LOW
    }
    public record CreateProjectTaskDTO
    {
        public required DateTime FromDate { get;  set; }
        public required string AssignedTo { get;  set; }
        public required DateTime ToDate { get;  set; }
        public required Priority Priority { get;  set; }
        public required int ProjectId { get;  set; }
        public required string Title { get; set; }
        public required string TaskDescription { get; set; }
    }
}