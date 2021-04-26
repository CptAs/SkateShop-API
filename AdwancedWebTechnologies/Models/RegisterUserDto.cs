using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Usernamae { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
    }
}
