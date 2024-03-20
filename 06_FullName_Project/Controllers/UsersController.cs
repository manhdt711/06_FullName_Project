
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

    }
}