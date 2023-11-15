using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers() 
        {
            var users = _userService.GetAll();
            return Ok(users);   
        }

        [HttpGet("{userId}")]
        public ActionResult GetUserById(int userId) 
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }
            _userService.Add(user);
            return Ok(user);
        }

        [HttpPut]
        public ActionResult UpdateUser(int userId, [FromBody] User user) 
        {
            if(user == null || userId != user.Id)
            {
                return BadRequest("Invalid user data");
            }
            _userService.Update(user);
            return Ok(user);
        }

        [HttpDelete]
        public ActionResult DeleteUser(int userId) 
        {
            _userService.Delete(userId);
            return Ok("Data successfully deleted");
        }
    }
}
