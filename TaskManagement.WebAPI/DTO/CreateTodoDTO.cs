namespace TaskManagement.WebAPI.DTO
{
    public record CreateTodoDTO
    {
        public required int ProjectTaskId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
