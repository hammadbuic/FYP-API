using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    [Table("Project")]
    public class project
    {

        public int projectId { get; set; }
        public string projectName { get; set; }
        public int projectRef { get; set; }
        public Group Group { get; set; }
    }
}
