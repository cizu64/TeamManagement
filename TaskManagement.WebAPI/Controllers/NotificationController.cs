using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;
using TaskManagement.WebAPI.DTO;

namespace TaskManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]
    public class NotificationController:ControllerBase
    {
        IGenericRepository<Notification> _notificationRepo;
        public NotificationController(IGenericRepository<Notification> notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            var notifications = await _notificationRepo.GetAll(n => n.UserId == userId);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotifications([FromBody] NotificationDTO dto)
        {
            await _notificationRepo.AddAsync(new Notification(dto.UserId, dto.Role, dto.Title, dto.Description));
            await _notificationRepo.UnitOfWork.SaveAsync();
            return Ok("Notifications added");
        }
    }
}
