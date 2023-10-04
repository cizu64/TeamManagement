namespace TaskManagement.WebAPI.DTO
{
    public record CreateProjectDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int TeamLeadId { get; set; }
        public string AssignedTeamMemberIds { get; set; }
    }
}