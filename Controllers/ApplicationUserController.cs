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
        public ApplicationUserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IOptions<AppConfig> appConfig)
        {
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
            ApplicationUser user = new ApplicationUser()
            {
                fullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        //POST: api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            try
            {
                if(user!=null && await _userManager.CheckPasswordAsync(user,model.Password))
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserID", user.Id.ToString())
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