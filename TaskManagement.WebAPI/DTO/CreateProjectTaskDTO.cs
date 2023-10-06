using System.ComponentModel.DataAnnotations;

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
        [Required]
        public DateTime FromDate { get;  set; }
        public string[]? AssignedTo { get;  set; }
        [Required]
        public required DateTime ToDate { get;  set; }
        public string Priority { get; set; } = "HIGH";
        [Required]
        public  int ProjectId { get;  set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string TaskDescription { get; set; }
    }
}