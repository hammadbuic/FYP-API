using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic_project_manager_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //Get Profile of student
        [HttpGet("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<Object> getStudent()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var student = await _db.students.Where(s => s.Id == userId).FirstOrDefaultAsync();
            return Ok(student);
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