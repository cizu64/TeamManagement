namespace TaskManagement.WebAPI.DTO
{
    public record CreateTeamMemberDTO
    {
        public required string LastName { get;  set; }
        public required int CountryId { get;  set; }
        public required int TeamLeadId { get;  set; }
        public required string Email { get;  set; }
        public required string FirstName { get;  set; }
        public required string Password { get;  set; }
    }
}