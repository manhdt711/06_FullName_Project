
using ClassLibrary2.ViewModel;
using DocumentFormat.OpenXml.Spreadsheet;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

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
        [HttpGet("GetUserByUserName/{userName}")]
        public ActionResult<User> GetUserById(string userName)
        {
            var user = UserDAO.FindUserByUserName(userName);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpGet("GetAllUser")]
        public ActionResult<User> GetAllUser()
        {
            return Ok(UserDAO.GetUsers());
        }
        [HttpPost("Create")]
        public IActionResult PostMember(User userRequest)
        {
            UserDAO.SaveUser(userRequest);
            return NoContent();
        }
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteMember(int id)
        {
            var p = UserDAO.FindUserById(id);
            if (p == null)
            {
                return NotFound();
            }
            UserDAO.DeleteUser(p);
            return NoContent();
        }
    }
}