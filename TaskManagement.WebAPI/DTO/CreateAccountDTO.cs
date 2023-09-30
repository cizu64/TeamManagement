namespace TaskManagement.WebAPI.DTO
{
    public record CreateAccountDTO
    {
        public required int CountryId { get; set; }
        public required string Email { get;  set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Password { get; set; }
    }
}