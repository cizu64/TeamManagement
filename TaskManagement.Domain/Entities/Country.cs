using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;
namespace TaskManagement.Domain.Entities
{
    public class Country:Entity
    {
        public Country(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        public bool IsActive { get; private set; } = true;
        
    }
}
