using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    public class Group
    {
        [Key]
        public int groupId { get; set; }
        public string groupName { get; set; }
        public project project { get; set; }
        public string supervisorId { get; set; }
        public virtual Supervisor Supervisor { get; set; }
        public  virtual ICollection<StudentModel> Student { get; set; }
    }
}
