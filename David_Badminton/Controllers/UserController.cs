using David_Badminton.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginDto)
        {
            try
            {
                var token = await _userService.LoginAsync(loginDto.Phone, loginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //---------------------------------------
        [HttpPost("login/student/")]
        public async Task<IActionResult> LoginStudent([FromBody] LoginRequestStudent loginDto)
        {
            try
            {
                var token = await _userService.LoginStudentAsync(loginDto.StudentCode, loginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        //---------------------------------------
    }

    public class LoginRequest
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    //-----------------------------------------------
    public class LoginRequestStudent
    {
        public string StudentCode { get; set; }
        public string Password { get; set; }
    }
    //-----------------------------------------------
}
