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
        public Supervisor Supervisor { get; set; }
    }
}
