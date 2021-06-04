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
    public class CoordinatorController : ControllerBase
    {
        AuthenticationContext _db;
        public CoordinatorController(AuthenticationContext db)
        {
            _db = db;
        }
        //Get List of supervisors
        [HttpGet("[action]")]
        [Authorize(Roles = "Coordinator")]
        public async Task<Object> getSupervisors()
        {
            //var supervisors = await _userManager.GetUsersInRoleAsync("Supervisor");
            var supervisors = await _db.supervisors.ToListAsync();
            return Ok(supervisors);
        }
        //Get List of students
        [HttpGet("[action]")]
        [Authorize(Roles = "Coordinator")]
        public async Task<Object> getStudents()
        {
            //var supervisors = await _userManager.GetUsersInRoleAsync("Supervisor");
            var students = await _db.students.ToListAsync();
            return Ok(students);
        }
        ////insert group
        //[HttpPost("[action]")]
        //[Authorize(Roles ="Coordinator")]
        //public async Task<Object> insertProject([FromBody] project project)
        //{
        //    var newProject=new project()
        //    {
        //        projectName=project.projectName,
        //    }
        //}
    }
}