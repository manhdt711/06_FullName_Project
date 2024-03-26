using ClassLibrary2.DTO;
using ClassLibrary2.ViewModel;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _06_FullName_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateController : ControllerBase
    {
        private readonly ElectricStore1Context _context;
        public DateController(ElectricStore1Context context)
        {
            _context = context;
        }
        [HttpGet("GetDateByOrder/{orderID}")]
        public IActionResult GetAllOrderDetaill(int orderID)
        {
            DateDTO dto = new DateDTO();
            if(dto == null)
            {
                return NotFound();
            }    
            return Ok(DateDAO.GetDateByOrderID(orderID));
        }
    }
}
