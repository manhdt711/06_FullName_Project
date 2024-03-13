
using ClassLibrary2.ViewModel;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace eStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommodityController : ControllerBase
    {
        [HttpGet("GetCommodityManager")]
        public ActionResult<IEnumerable<ClassLibrary2.DTO.CommodityDTO>> GetAllOrderDetaill()
        {
            return CommondityDtoDAO.GetCommodityDTO();
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            CommodityDAO.DeleteCommodityById(id);
            return NoContent();
        }
        [HttpPut("Update/{id}")]
        public IActionResult UpdateProduct(Commodity c)
        {
            CommodityDAO.UpdateCommodity(c);
            return NoContent();
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ClassLibrary2.DTO.CommodityDTO commodityDTO = CommondityDtoDAO.GetCommodityDTOById(id);
            return Ok(commodityDTO);
        }

    }
}
