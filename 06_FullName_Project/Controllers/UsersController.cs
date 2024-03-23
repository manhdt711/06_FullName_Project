
using ClassLibrary2.ViewModel;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("GetUserById/{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = UserDAO.FindUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] ElecStore.Models.User user)
        {
            User user1 = UserDAO.Login(user);
            return Ok(user1);
        }
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.UserId)
            {
                return BadRequest();
            }

            var existingUser = UserDAO.FindUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = updatedUser.UserName;
            existingUser.Email = updatedUser.Email;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;

            UserDAO.UpdateUser(existingUser);

            return NoContent();
        }
    }
}