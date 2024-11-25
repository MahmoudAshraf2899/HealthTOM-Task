using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Contracts.DTOs.Setter.Patient
{
    public class PatientSetterDto
    {
        public string Name { get; set; }
        
        public int Gender { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string Email { get; set; }
    }
}
