using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic_project_manager_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academic_project_manager_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly UserManager<Supervisor> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        AuthenticationContext _db;
        public AdminController(AuthenticationContext db,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //GET List of Available Supervisors
        [HttpGet("[action]")]
        public IActionResult listUsers()
        {
            
            return Ok(_userManager.Users);
        }
        //GET List of Roles available
        [HttpGet("[action]")]
        public IActionResult getRoles()
        {

            return Ok(_roleManager.Roles);
        }
        //Get List of supervisors
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin,Coordinator")]
        public async Task<Object> getSupervisors()
        {
            //var supervisors = await _userManager.GetUsersInRoleAsync("Supervisor");
            var supervisors = await _db.supervisors.ToListAsync();
            return Ok(supervisors);
        }
        //Adding a new Supervisor
        //localhost:5484/api/admin/insertsupervisor
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<Object> insertSupervisor([FromBody] Supervisor supervisor)
        {
            string pass = "supervisor123";
            string role = "Supervisor";
            var newSupervisor = new Supervisor()
            {
                fullName = supervisor.fullName,
                UserName = supervisor.UserName,
                Email = supervisor.Email,
                PhoneNumber = supervisor.PhoneNumber,
                designation = supervisor.designation,
                department = supervisor.department,
                program = supervisor.program,
                gitID=supervisor.gitID,
                webURL=supervisor.webURL,
                createdAt=supervisor.createdAt
            };
            try
            {
                
                var result = await _userManager.CreateAsync(newSupervisor,pass );
                 await _userManager.AddToRoleAsync(newSupervisor, role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Updating a Supervisor
        //api/admin/updatesupervisor/id
        [HttpPut("[action]/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> updateSupervisor([FromRoute] string id,[FromBody] Supervisor supervisor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var findSupervisor = _db.supervisors.FirstOrDefault(p => p.Id == id);
            if (findSupervisor == null)
            {
                return NotFound();
            }
            findSupervisor.fullName = supervisor.fullName;
            findSupervisor.UserName = supervisor.UserName;
            findSupervisor.Email = supervisor.Email;
            findSupervisor.PhoneNumber = supervisor.PhoneNumber;
            findSupervisor.program = supervisor.program;
            findSupervisor.designation = supervisor.designation;
            findSupervisor.department = supervisor.department;
            _db.Entry(findSupervisor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("Supervisor with id: " + id + "is Updated"));
        }
        //Deleting a Supervisor
        //api/admin/updatesupervisor/id
        [HttpDelete("[action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteSupervisor([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //deleting from coordinator table if present
            var findCoordinator = _db.Coordinators.FirstOrDefault(p => p.supervisorId == id);
            if (findCoordinator != null)
            {
                var students = _db.students.Where(p => p.coordinatorId == findCoordinator.Id).ToList();
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        if (student.coordinatorId != null)
                        {
                            student.coordinatorId = null;
                        }
                    }
                }
                _db.Coordinators.Remove(findCoordinator);
            }
                //await _db.SaveChangesAsync();
            var findSupervisor = _db.supervisors.FirstOrDefault(p => p.Id == id);
            if (findSupervisor == null)
            {
                return NotFound();
            }
            _db.supervisors.Remove(findSupervisor);
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("Supervisor with id: " + id + "is Deleted"));
        }
        //Adding coordinator table
        //api/admin/make coordinator/id
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> makeCoordinator([FromBody] Coordinator coordinator)
        {
            var newCoordinator = new Coordinator
            {
                section = coordinator.section,
                supervisorId=coordinator.supervisorId,
                gitId=coordinator.gitId,
                web_url=coordinator.web_url,
                groupName=coordinator.groupName,
                groupPath=coordinator.groupPath,
                description=coordinator.description,
                created_at=coordinator.created_at,
                reposId=coordinator.reposId,
                reposName=coordinator.reposName,
                reposUrl=coordinator.reposUrl
            };
            var findSupervisor = _db.Users.FirstOrDefault(p => p.Id == coordinator.supervisorId);
            await _db.Coordinators.AddAsync(newCoordinator);
            var result=await _userManager.AddToRoleAsync(findSupervisor, "Coordinator");
            var fSupervisor = _db.supervisors.FirstOrDefault(p => p.Id == coordinator.supervisorId);
            if (findSupervisor == null)
            {
                return NotFound();
            }
            fSupervisor.is_coordiantor = true;
            _db.Entry(fSupervisor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("Supervisor with id: " + coordinator.supervisorId + " is added in coordinator table\n"+result));
        }
        //Get List of students
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin,Coordinator")]
        public async Task<Object> getStudents()
        {
            //var supervisors = await _userManager.GetUsersInRoleAsync("Supervisor");
            var students = await _db.students.ToListAsync();
            return Ok(students);
        }
        //Adding a new Student
        //localhost:5484/api/admin/insertstudent
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<Object> insertStudent([FromBody] StudentModel student)
        {
            string pass = "student123";
            string role = "Student";
            var newStudent = new StudentModel()
            {
                fullName = student.fullName,
                UserName = student.UserName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                registrationNumber = student.registrationNumber,
                fatherName=student.fatherName,
                address=student.address,
                department = student.department,
                program = student.program,
                gitID = student.gitID,
                webURL = student.webURL,
                createdAt = student.createdAt
            };
            try
            {

                var result = await _userManager.CreateAsync(newStudent, pass);
                await _userManager.AddToRoleAsync(newStudent, role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Updating a Student
        //api/admin/updatestudent/id
        [HttpPut("[action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateStudent([FromRoute] string id, [FromBody] StudentModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var findStudent = _db.students.FirstOrDefault(p => p.Id == id);
            if (findStudent == null)
            {
                return NotFound();
            }
            findStudent.fullName = student.fullName;
            findStudent.UserName = student.UserName;
            findStudent.Email = student.Email;
            findStudent.PhoneNumber = student.PhoneNumber;
            findStudent.program = student.program;
            findStudent.registrationNumber = student.registrationNumber;
            findStudent.fatherName = student.fatherName;
            findStudent.address = student.address;
            findStudent.department = student.department;
            _db.Entry(findStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("Student with id: " + id + "is Updated"));
        }
        //Deleting a Student
        //api/admin/updatestudent/id
        [HttpDelete("[action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteStudent([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var findStudent = _db.students.FirstOrDefault(p => p.Id == id);
            if (findStudent == null)
            {
                return NotFound();
            }
            _db.students.Remove(findStudent);
            //deleting from coordinator table if present
            //var findCoordinator = _db.Coordinators.FirstOrDefault(p => p.supervisorId == id);
            //if (findCoordinator != null)
            //{
            //    _db.Coordinators.Remove(findCoordinator);

            //}
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("Student with id: " + id + "is Deleted"));
        }
        
    }
}