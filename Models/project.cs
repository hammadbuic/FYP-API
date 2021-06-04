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
        public string projectDescription { get; set; }
        public int groupId { get; set; }
        public Group Group { get; set; }
        //update database
    }
}
