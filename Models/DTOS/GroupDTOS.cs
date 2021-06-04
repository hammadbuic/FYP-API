using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models.DTOS
{
    public class GroupDTOS
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public string supervisorId { get; set; }
        public int projectId { get; set; }
        public string projectName { get; set; }
        public string projectDescription { get; set; }
    }
}
