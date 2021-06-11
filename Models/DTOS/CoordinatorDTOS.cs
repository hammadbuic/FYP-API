using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models.DTOS
{
    public class CoordinatorDTOS
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
    }
}
