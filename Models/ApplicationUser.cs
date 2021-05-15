using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName ="nvarchar(150)")]
        public string fullName { get; set; }
        public string program { get; set; }
        public string department { get; set; }
        public int gitID { get; set; }
        public string webURL { get; set; }
        public DateTime createdAt { get; set; }

    }
}
