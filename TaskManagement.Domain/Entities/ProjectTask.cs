﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{

    public class ProjectTask:Entity
    {
        
        public ProjectTask(int projectId, string taskDescription, string priority, string assignedTo, DateTime fromDate, DateTime toDate)
        {
            ProjectId = projectId;
            TaskDescription = taskDescription;
            Priority = priority;
            AssignedTo = assignedTo;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public int ProjectId { get; private set; }
        public string TaskDescription { get; private set; }
        public string Priority { get; private set; }
        public string AssignedTo { get; private set; } //assign to many team members
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsCompleted { get; private set; } = false;
        public DateTime DateCreated { get; private set; } = DateTime.Now;

        //child entity
        public Project Project { get; private set; }
        //navigational property
        private readonly List<Todo> todo = new();
        public IReadOnlyCollection<Todo> Todo => todo.AsReadOnly();
    }
}