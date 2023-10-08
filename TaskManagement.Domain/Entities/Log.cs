using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{
    public class Log:Entity
    {
        public Log(string message, string shortMessage="")
        {
            Message = message;
            ShortMessage = shortMessage;
        }

        public string Message { get; private set; }
        public string? ShortMessage { get; private set; }
        public DateTime DateLogged { get; private set; } = DateTime.Now;
    }
}
