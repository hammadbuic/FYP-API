using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    [Table("Coordinator")]
    public class Coordinator
    {
        public int Id { get; set; }
        public string section { get; set; }
        public string supervisorId { get; set; }
        public int gitId { get; set; }
        public string web_url { get; set; }
        public string groupName { get; set; }
        public string groupPath { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public string reposName { get; set; }
        public int reposId { get; set; }
        public string reposUrl { get; set; }
        public Supervisor Supervisor { get; set; }
        public virtual ICollection<Group> Groups {get;set;}
        public virtual ICollection<StudentModel> Student { get; set; }
        public virtual ICollection<Activities> Activities { get; set; }
    }
}
