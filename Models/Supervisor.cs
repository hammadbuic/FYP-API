using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    [Table("Supervisors")]
    public class Supervisor:ApplicationUser
    {
        public string designation { get; set; }
        //public string department { get; set; }
        //public string program { get; set; }
        public bool is_coordiantor { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual Coordinator coordinator { get; set; }
    }
}
