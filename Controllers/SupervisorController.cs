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
    public class SupervisorController : ControllerBase
    {
        private readonly AuthenticationContext _db;
        public SupervisorController(AuthenticationContext db)
        {
            _db = db;
        }
        //Get Profile of student
        [HttpGet("[action]")]
        [Authorize(Roles = "Supervisor,Coordinator")]
        public async Task<Object> getSupervisor()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var supervisor = await _db.supervisors.Where(s => s.Id == userId).FirstOrDefaultAsync();
            return Ok(supervisor);
        }
    }
}