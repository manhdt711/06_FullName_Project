
using ClassLibrary2.ViewModel;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly ElectricStore1Context _context;
        public UserController(ElectricStore1Context context)
        {
            _context = context;
        }
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
        [HttpGet("Login")]
        public ActionResult<User> Login([FromBody] ElecStore.Models.User user)
        {
            User user1 = _context.Users.FirstOrDefault(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
            return Ok(user);
        }

    }
}
