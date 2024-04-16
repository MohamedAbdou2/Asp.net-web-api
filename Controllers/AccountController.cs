using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //////resource user
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            _userManager = userManager;
            this.config = config;
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

                        ///claims token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        //role

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }


                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                        SigningCredentials signincred = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);



                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],  //url web api 
                            audience: config["JWT:ValidAudiance"], //url consumer angular
                            claims: claims,
                            expires:DateTime.Now.AddHours(1),
                            signingCredentials:signincred

                            );

                        return Ok(
                            
                            new
                            {
                                token= new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expiration =mytoken.ValidTo

                            }
                            
                            
                            );

                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }

            return Unauthorized();
        }



    }
}
