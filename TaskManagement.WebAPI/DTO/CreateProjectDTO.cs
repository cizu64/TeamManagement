namespace TaskManagement.WebAPI.DTO
{
    public record CreateProjectDTO
    {
        public required string Name { get; set; }
        public required int TeamLeadId { get; set; }
        public required string AssignedTeamMemberIds { get; set; }
    }
}