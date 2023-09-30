using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.SeedWork
{
    public class Logger
    {
        public static Queue<Log> Logs = new(); //A queue to store all logs

        /// <summary>
        /// A static method add log objects to the queue 
        /// </summary>
        /// <param name="log"></param>
        public static void LogIt(Log log)
        {
            Logs.Enqueue(log);
        }
    }
}
