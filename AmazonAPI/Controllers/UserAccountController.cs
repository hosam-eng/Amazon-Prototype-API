using Amazon.Application.Contracts;
using Amazon.Application.Services;
using Amazon.Domain;
using Amazon.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace Amazon.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class UserAccountController : ControllerBase
	{
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserReposatory userReposatory;

        public UserAccountController(
            IConfiguration configuration,IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserReposatory userReposatory)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userReposatory = userReposatory;
        }

        [Route("Login/")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            var user = new ApplicationUser();
            if (userLogin.EmailAddress!=null)
            {
                user = await userReposatory.LoginByEmail(userLogin.EmailAddress);
            }
            else if (userLogin.Phone!=null)
            {
                user = await userReposatory.LoginByPhoneNumber(userLogin.Phone);
            }
            
            if (user != null)
            {              
                var result = await signInManager.PasswordSignInAsync(user, userLogin.Password, true, false);
                if (result.Succeeded)
                {
                    var id = await userManager.GetUserIdAsync(user);
                    return RedirectToAction("GenerateToken", new {id=id});
                }
            }
            return NotFound();
        }
        [Route("Register/")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO userDto)
        {
            var user = mapper.Map<ApplicationUser>(userDto);
            try
            {
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    var id = await userManager.GetUserIdAsync(user);
                    var claims = new List<Claim>
                       {
                           new Claim(ClaimTypes.Name, user.UserName),
                           new Claim(ClaimTypes.NameIdentifier,id)
                       };
                    await signInManager.SignInWithClaimsAsync(user, false, claims);
                    return Ok(user);
                }
                else
                {
                    string errors = "";
                    foreach (var error in result.Errors)
                    {
                        errors += error.Description.ToString();
                    }
                    return BadRequest(errors);
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            } 
          
        } 

        [Route("LogOut/")]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [Route("Token/")]
        [HttpGet]
        public async Task<IActionResult> GenerateToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("SecretKey").Value);

            if (id != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, "hosam"),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.NameIdentifier, id)
            }),
                    Expires = DateTime.UtcNow.AddMonths(1),
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha384Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var response = new
                {
                    userId = id,
                    token = tokenString
                };
                return Ok(response);
            }
            else
                return BadRequest();
        }

    }
}
