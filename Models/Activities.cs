using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    public class Activities
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string branch { get; set; }
        public int? coordinatorId { get; set; }
        public Coordinator Coordinator { get; set; }
    }
}
