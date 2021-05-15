using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    public class ApplicationUserModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string registrationNumber { get; set; }
        public string fatherName { get; set; }
        public string address { get; set; }
        public string program { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        //public string program { get; set; }
        public string Password { get; set; }
    }
}
