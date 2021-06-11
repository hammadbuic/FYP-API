using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic_project_manager_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academic_project_manager_WebAPI.Models.DTOS;
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
        // GET: api/Groups/5
        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<CoordinatorDTOS>> GetCoordinator()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var coordinator = await _db.Coordinators.Where(c => c.supervisorId == userId).FirstOrDefaultAsync();

            if (coordinator == null)
            {
                return NotFound();
            }
            return CoordinatorToDTOS(coordinator);
        }
         private static CoordinatorDTOS CoordinatorToDTOS(Coordinator coordinator) =>
           new CoordinatorDTOS
           {
               Id = coordinator.Id,
               section = coordinator.section,
               supervisorId = coordinator.supervisorId,
               gitId = coordinator.gitId,
               web_url = coordinator.web_url,
               groupName = coordinator.groupName,
               groupPath = coordinator.groupPath,
               description= coordinator.description,
               created_at= coordinator.created_at,
               reposName= coordinator.reposName,
               reposId= coordinator.reposId,
               reposUrl=coordinator.reposUrl
           };
    }
}