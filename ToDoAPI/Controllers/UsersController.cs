using Microsoft.AspNetCore.Mvc;
using ToDoAPI.DTO;
using ToDoAPI.Helpers;
using ToDoAPI.Interface;
using ToDoAPI.Model;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model) 
        {
            var response = await _userService.Authenticate(model);

            if (response == null) return BadRequest(new { message = "Username or password is incorrect" });



            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DTOUser>>> GetAllUsers() 
        {
            return Ok(await _userService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User userObj) 
        {
            userObj.Id = 0;
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] User userObj) 
        {
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }
    }
}
