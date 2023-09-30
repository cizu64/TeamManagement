namespace TaskManagement.WebAPI.DTO
{
    public record SignInDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}