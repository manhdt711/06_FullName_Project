
using ClassLibrary2.ViewModel;
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

    }
}
