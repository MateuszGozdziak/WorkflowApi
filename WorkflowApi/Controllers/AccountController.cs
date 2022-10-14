using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;
//using System.Web.Http;
using WorkflowApi.Services;

namespace WorkflowApi.Controllers
{
    //[AutoValidateAntiforgeryToken]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, ILogger<AccountController> logger, IJwtTokenService jwtTokenService)
        {
            this._logger = logger;
            this._jwtTokenService = jwtTokenService;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {

            if (await UserExists(registerUserDto.Email)) { return BadRequest("Username is taken"); }
            
            AppUser newUser = new()
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.Email
            };

            var result = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            Tuple<string, DateTime> TokenAndExpireTime = await _jwtTokenService.GenerateJwt(newUser);

            UserDto userDto = new UserDto()
            {
                Email = newUser.Email,
                Id = newUser.Id,
                Token = TokenAndExpireTime.Item1,
                ExpireTime = TokenAndExpireTime.Item2
            };

            return Ok(userDto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Email == loginUserDto.Email);

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            Tuple<string, DateTime> TokenAndExpireTime = await _jwtTokenService.GenerateJwt(user);

            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                Id = user.Id,
                Token = TokenAndExpireTime.Item1,
                ExpireTime = TokenAndExpireTime.Item2
            };

            return Ok(userDto);
        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username);
        }
    }
}
