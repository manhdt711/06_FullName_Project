using ClassLibrary2.ViewModel;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace _06_FullName_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly ElectricStore1Context _context;
        public PromotionController(ElectricStore1Context context)
        {
            _context = context;
        }

        [HttpGet("GetAllPromotions")]
        public ActionResult<IEnumerable<ElecStore.Models.Promotion>> GetAllPromotions()
        {
           return _context.Promotions.ToList();
        }
        [HttpGet("Delete/{id}")]
        public IActionResult DeletePromotion(int id)
        {
            Promotion promotion = _context.Promotions.FirstOrDefault(x => x.PromotionId == id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                _context.SaveChanges();
            }
            return NoContent();
        }
        [HttpPost("Update")]
        public IActionResult UpdatePromotion(Promotion c)
        {
            _context.Promotions.Add(c);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost("Create")]
        public IActionResult CreatePromotion(Promotion c)
        {
            _context.Promotions.Add(c);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
