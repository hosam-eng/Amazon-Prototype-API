using Amazon.Application.Services;
using Amazon.Domain;
using Amazon.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }
        [HttpGet]
        [Route("getUserById")]
        public async Task<IActionResult> getById(string id)
        {
            var user = await userService.getUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> updateUser(string id, UserProfileDTO userProfileDTO)
        {
            var res = await userService.UpdateAsync(id, userProfileDTO);
            if (res)
            {
                return Ok(res);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("changePassword")]

        public async Task<IActionResult> changePassword(ChangePasswordDTO passwordDTO)
        {
            var user = await userManager.FindByIdAsync(passwordDTO.userId);
            if (user != null)
            {
                var checkpassres = await userManager.CheckPasswordAsync(user, passwordDTO.oldPassword);
                if (checkpassres)
                {
                    var Change = await userManager.ChangePasswordAsync(user, passwordDTO.oldPassword, passwordDTO.newPassword);
                    if (Change.Succeeded)
                    {
                        return Ok(Change);
                    }
                    else
                    {
                        return BadRequest("somthing wrong");
                    }
                }
            }
            return BadRequest("enter valid password");
        }
    }
}
