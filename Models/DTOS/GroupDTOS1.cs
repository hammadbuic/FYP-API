using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models.DTOS
{
    public class GroupDTOS1
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public string supervisorId { get; set; }
        public string username { get; set; }
        public int projectId { get; set; }
        public string projectName { get; set; }
        public string projectDescription { get; set; }
        public int gitProjectId { get; set; }
        public DateTime created_at { get; set; }
        public string http_url_to_repo { get; set; }
    }
}
