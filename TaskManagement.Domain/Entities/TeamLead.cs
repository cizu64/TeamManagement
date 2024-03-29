﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{
    public class TeamLead:Entity
    {
        public TeamLead(int countryId, string email, string firstname, string lastname, string password)
        {
            CountryId = countryId;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
        }
        public int CountryId { get; private set; }
        public string Email { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;
        public string Role { get; private set; } = nameof(TeamLead);
        public Country Country { get; private set; }
        //navigational property
        private readonly List<Project> projects = new();
        public IReadOnlyCollection<Project> Projects => projects.AsReadOnly();

        //navigational property for TeamMember because TeamLeads can have many TeamMembers
        private readonly List<TeamMember> teamMembers = new();
        public IReadOnlyCollection<TeamMember> TeamMembers => teamMembers.AsReadOnly();
    }
}
