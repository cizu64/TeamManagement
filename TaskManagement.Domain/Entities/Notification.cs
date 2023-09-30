using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{
    public class Notification:Entity
    {

        public Notification(int userId, string role, int title, int description)
        {
            UserId = userId;
            Role = role;
            Title = title;
            Description = description;
        }
        public int UserId { get; private set; }

        public string Role { get; private set; }
        public int Title { get; private set; }
        public int Description { get; private set; }      
        public DateTime DateCreated { get; private set; } = DateTime.Now;
    }
}
