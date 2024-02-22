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
        [Required(ErrorMessage ="From Date is Required")]
        public DateTime FromDate { get;  set; }
        public string[]? AssignedTo { get;  set; }
        [Required(ErrorMessage ="To Date is required")]
        public required DateTime ToDate { get;  set; }
        public string Priority { get; set; } = "HIGH";
        [Required(ErrorMessage ="Project id is required")]
        public  int ProjectId { get;  set; }
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Task description is required")]
        public string TaskDescription { get; set; }
    }
}