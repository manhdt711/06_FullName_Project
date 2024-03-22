
using ClassLibrary2.DTO;
using ClassLibrary2.ViewModel;
using DocumentFormat.OpenXml.Office2010.Excel;
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
        [HttpPut("Update")]
        public IActionResult UpdateCommodity([FromBody] Commodity commodity)
        {
            if (commodity == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                CommodityDAO.UpdateCommodity(commodity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ClassLibrary2.DTO.CommodityDTO commodityDTO = CommondityDtoDAO.GetCommodityDTOById(id);
            return Ok(commodityDTO);
        }

        [HttpGet("GetByCommodityName/{commodityName}")]
        public IActionResult GetByCommodityName(string commodityName)
        {
            List<ClassLibrary2.DTO.CommodityDTO> commodityDTO = CommondityDtoDAO.GetCommodityDTOByName(commodityName);
            return Ok(commodityDTO);
        }
        [HttpGet("GetByCategoryId/{categoryId}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            if (categoryId == -1)
            {
                return Ok(CommondityDtoDAO.GetCommodityDTO());
            }
            else
            {
                List<ClassLibrary2.DTO.CommodityDTO> commodityDTO = null;
                commodityDTO = CommondityDtoDAO.GetCommodityDTOByCategoryId(categoryId);
                return Ok(commodityDTO);
            }
        }
        [HttpPost("CreateCommodity")]
        public IActionResult PostCommodity(Commodity c)
        {
            CommodityDAO.SaveCommodity(c);
            return NoContent();
        }
    }
}
