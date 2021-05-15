using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic_project_manager_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academic_project_manager_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AuthenticationContext _db;
        public StudentController(AuthenticationContext db)
        {
            _db = db;
        }

        //GET api/studentList
        [HttpGet("[action]")]
        public IActionResult GetStudents()
        {
            return Ok(_db.students.ToList());
        }
        ////GET api/studentList
        //[HttpGet("[action]")]
        //public IActionResult GetAllProjects([FromRoute] int id)
        //{
        //    //return Ok(_db.projects.ToList());
        //}
        ////Post api/activity
        //[HttpGet("[action]/{id}")]
        //public IActionResult getDetailsById([FromRoute] int id)
        //{
        //    var findUser = _db.StudentModels.FirstOrDefault(p => p.projectId == id);
        //    return Ok(findUser);
        //}
    }
}