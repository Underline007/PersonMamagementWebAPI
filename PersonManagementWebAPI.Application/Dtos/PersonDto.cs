using PersonManagementWebAPI.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagementWebAPI.Application.Dtos
{
    public class PersonDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public GenderType Gender { get; set; }
        [Required]
        public string BirthPlace { get; set; } = string.Empty;
    }
}
