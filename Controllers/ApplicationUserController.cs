using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Academic_project_manager_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Academic_project_manager_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        //injecting applicationuser
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly AppConfig _appConfig;
        private readonly AuthenticationContext _db;
        public ApplicationUserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IOptions<AppConfig> appConfig,AuthenticationContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _appConfig = appConfig.Value;
        }
        //WebAPI Method
        [HttpPost]
        [Route("Register")]
        //POST: api/ApplicationUser/Register
        public async Task<Object> postApplicationUser(ApplicationUserModel model)
        {
            //consume applicationusermodel
            //to insert in IDentity model Application user obj required
            //ApplicationUser user = new ApplicationUser()
            //{
            //    fullName = model.FullName,
            //    UserName = model.UserName,
            //    Email = model.Email,
            //    PhoneNumber = model.PhoneNumber,

            //};
            if (model.Role == "Student")
            {
                StudentModel student = new StudentModel()
                {
                    fullName = model.FullName,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    registrationNumber = model.registrationNumber,
                    fatherName = model.fatherName,
                    address = model.address,
                    program = model.program
                };
                try
                {
                    var result = await _userManager.CreateAsync(student, model.Password);
                    await _userManager.AddToRoleAsync(student, model.Role);
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else if(model.Role=="Supervisor")
            {
                Supervisor supervisor = new Supervisor()
                {
                    fullName = model.FullName,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    designation = model.designation,
                    department = model.department,
                    program = model.program
                };
                try
                {
                    var result = await _userManager.CreateAsync(supervisor, model.Password);
                    await _userManager.AddToRoleAsync(supervisor, model.Role);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (model.Role=="Admin")
            {
                ApplicationUser application = new ApplicationUser()
                {
                    fullName = model.FullName,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                try
                {
                    var result = await _userManager.CreateAsync(application, model.Password);
                    await _userManager.AddToRoleAsync(application, model.Role);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("StudentDetails")]
        //POST: api/ApplicationUser/StudentDetails
        public async Task<IActionResult> saveStudentDetails(StudentModel student)
        {
            //var studentInformation = _db.students.FirstOrDefault(p => p.Id == currentId);
            //if (studentInformation == null)
            //{
                StudentModel stu = new StudentModel()
                {
                    Id = student.Id,
                    registrationNumber = student.registrationNumber,
                    fatherName = student.fatherName,
                    address = student.address,
                    program = student.program,
                };
                _db.students.Add(stu);
                await _db.SaveChangesAsync();
            //}
            return Ok();
        }
        [HttpPost]
        [Route("Login")]
        //POST: api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            
            IdentityOptions _options = new IdentityOptions();
            try
            {
                if(user!=null && await _userManager.CheckPasswordAsync(user,model.Password))
                {
                    var role = await _userManager.GetRolesAsync(user);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserID", user.Id.ToString()),
                            new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { message = "username or password is incorrect" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}