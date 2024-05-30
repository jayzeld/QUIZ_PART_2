using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDiscussion.Models;
using WebAPIDiscussion.Utils;

namespace WebAPIDiscussion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthorize]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            //if(!HttpContext.Request.Headers.TryGetValue("X-AUF-API-KEY", out var extractedApiKey))
            //{
            //    return StatusCode(StatusCodes.Status401Unauthorized, new ResponseModel { Status = "Error", Message = "Access Denied" });
            //}

            //var apiKey = "HELLOAPIKEY";
            //if (!apiKey.Equals(extractedApiKey))
            //{
            //    return StatusCode(StatusCodes.Status401Unauthorized, new ResponseModel { Status = "Error", Message = "Access Denied" });
            //}

           

            var userExist = await _userManager.FindByNameAsync(model.Username);
            if (userExist != null) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exist" });
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (!res.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed" });
            }

            if(!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            await _userManager.AddToRoleAsync(user,UserRoles.Admin);

            return Ok(new ResponseModel { Status = "Success", Message = "Admin created successfully" });
        }
    }
}
