using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.SeedWork
{
    public record LogData(string Message, string ShortMessage);
    
    public class Logger
    {
        public static Queue<LogData> Logs = new(); //A queue to store all logs

        /// <summary>
        /// A static method add log objects to the queue 
        /// </summary>
        /// <param name="log"></param>
        public static void LogIt(LogData log)
        {
            Logs.Enqueue(log);
        }
    }
}
