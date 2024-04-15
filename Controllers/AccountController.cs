using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //////resource user
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        // I wanna make 
        //create Account new user "registration" "post" 
        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = userDto.UserName;
                user.Email = userDto.Email;

             IdentityResult result =  await  _userManager.CreateAsync(user,userDto.Password);

                if (result.Succeeded)
                {
                    return Ok("account added successfully");
                }

                var errorMessages = new List<string>();

                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.Description);

                }
               
                return BadRequest(errorMessages) ;

            }
            return BadRequest(ModelState);    
        }

        //check Account valid "Login" "post"

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
             ApplicationUser user = await _userManager.FindByNameAsync(userDto.UserName);
                if (user != null)
                {
                 bool found = await _userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {

                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }

            return Unauthorized();
        }



    }
}
