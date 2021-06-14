using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    public class StudentFileUpload
    {
        public int id { get; set; }
        public string filename { get; set; }
        public string file_type { get; set; }
        public string branch { get; set; }
        public int? activitiesId { get; set; }
        public int? groupId { get; set; }
        public Activities Activities { get; set; }
        public Group Group { get; set; }
    }
}
