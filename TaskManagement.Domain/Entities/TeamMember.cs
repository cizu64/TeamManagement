using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Domain.Entities
{
    public class TeamMember:Entity
    {      
        public TeamMember(int countryId, int teamLeadId, string email, string firstname, string lastname, string password)
        {
            CountryId = countryId;
            TeamLeadId = teamLeadId;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
        }

        public int CountryId { get; private set; }
        public int TeamLeadId { get; private set; }
        public string Email { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime DateCreated { get; private set; } = DateTime.Now;
        public string Role { get; private set; } = nameof(TeamMember); 
    }
}
