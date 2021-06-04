using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    [Table("Students")]
    public class StudentModel:ApplicationUser
    {
        public string registrationNumber { get; set; }
        public string fatherName { get; set; }
        public string address { get; set; }
        //public string program { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }
        public int? coordinatorId { get; set; }
        public Coordinator Coordinator { get; set; }
    }
}
