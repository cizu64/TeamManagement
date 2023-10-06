using System.ComponentModel.DataAnnotations;

namespace TaskManagement.WebAPI.DTO
{
    public record CreateProjectDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        public string[]? AssignedTeamMemberIds { get; set; }
    }
}