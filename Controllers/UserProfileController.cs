using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic_project_manager_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;

namespace Academic_project_manager_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        // GET: api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Id,
                user.fullName,
                user.UserName,
                user.Email,
                user.PhoneNumber,
            };
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        [Route("Admin")]
        public async Task<Object> getForAdmin()
        {
            List<ApplicationUserModel> supervisorList = new List<ApplicationUserModel>();
            var items = await _userManager.GetUsersInRoleAsync("Supervisor");
            //return "Web Method for admin";
            foreach(var item in items)
            {
                supervisorList.Add(new ApplicationUserModel { FullName = item.fullName, UserName = item.UserName, Email = item.Email, PhoneNumber = item.PhoneNumber });
            }
            return supervisorList;
        }


        [HttpGet]
        [Authorize(Roles = "Coordinator")]
        [Route("Coordinator")]
        public string getForCoordinator()
        {
            return "Web Method for Coordinator";
        }


        [HttpGet]
        [Authorize(Roles = "Student")]
        [Route("Student")]
        public string getForStudent()
        {
            return "Web Method for Student";
        }


        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        [Route("Supervisor")]
        public string getForSupervisor()
        {
            return "Web Method for Supervisor";
        }
    }
}