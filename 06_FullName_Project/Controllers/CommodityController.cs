
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

    }
}
