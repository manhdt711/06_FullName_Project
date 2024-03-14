
using ClassLibrary2.ViewModel;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace eStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommodityCategoryController : ControllerBase
    {
        [HttpGet("GetCommodityCategory")]
        public ActionResult<IEnumerable<CommodityCategory>> GetAllOrderDetaill()
        {
            return CommodityCategoryDAO.GetCommodityCategory();
        }

    }
}
