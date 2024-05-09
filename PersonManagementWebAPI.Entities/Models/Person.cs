using PersonManagementWebAPI.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagementWebAPI.Entities.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime DateOfBirth {  get; set; }  
        public GenderType Gender { get; set; }
        public string BirthPlace { get; set; } = string.Empty;

    }
}
