namespace TaskManagement.WebAPI.DTO
{
    public record NotificationDTO
    {
        public required int UserId { get;  set; }
        public required string Role { get; set; }
        public required int Title { get;  set; }
        public required int Description { get; set; }
    }
}
