//using Application.Interfaces;
//using Domain.Entities;
//using Domain.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userService;

//        public UserController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        [HttpGet]
//        public ActionResult<IEnumerable<User>> GetAllUsers()
//        {
//            var users = _userService.GetAllUsers();
//            return Ok(users);
//        }

//        [HttpGet("Traer por {id}")]
//        public ActionResult<User> GetUserById(int id)
//        {
//            var user = _userService.GetUserById(id);
            
//            if (user == null)
//            {
//                return NotFound();
//            }

//            return Ok(user);
//        }

//        //[HttpPost]
//        //public IActionResult AddUser([FromBody] User user)
//        //{

//        //    try
//        //    {
//        //        _userService.AddUser(user);
//        //        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
//        //    }
//        //    catch (InvalidOperationException ex)
//        //    {
//        //        return BadRequest(new { message = ex.Message });
//        //    }

//        //}

//        //[HttpPut("{id}")]
//        //public IActionResult UpdateUser(int id, [FromBody] User user)
//        //{

//        //    if (id != user.Id)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    try
//        //    {
//        //        _userService.UpdateUser(user);
//        //    }
//        //    catch (Exception)
//        //    {
//        //        return NotFound();
//        //    }

//        //    return NoContent();
//        //}

//        [HttpDelete("{id}")]
//        public IActionResult DeleteUser(int id)
//        {
//            var user = _userService.GetUserById(id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            _userService.DeleteUser(id);
//            return NoContent();
//        }
//    }
//}
