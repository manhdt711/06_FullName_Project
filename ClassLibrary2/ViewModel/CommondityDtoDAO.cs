using ClassLibrary2.DTO;
using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
namespace ClassLibrary2.ViewModel
{
    public class CommondityDtoDAO
    {
        public static List<CommodityDTO> GetCommodityDTO()
        {
            var commodities = new List<CommodityDTO>();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    var commodityList = context.Commodities
                        .Include(c => c.Category)
                        .OrderBy(c => c.Category.CategoryName)
                        .Select(c => new CommodityDTO
                        {
                            CommodityId = c.CommodityId,
                            CommodityName = c.CommodityName,
                            UnitPrice = c.UnitPrice ?? 0, // Handling nullable value
                            UnitInStock = c.UnitInStock ?? 0, // Handling nullable value
                            CategoryName = c.Category != null ? c.Category.CategoryName : "Uncategorized" // Handling null category without null propagating operator
                        })
                        .ToList();

                    commodities.AddRange(commodityList);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commodities;
        }
        //get commodity by id (func btn buy commodity to show modal)

        public static CommodityDTO GetCommodityDTOById(int id)
        {
            CommodityDTO commodityDTO = new CommodityDTO();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    Commodity commodity = context.Commodities.Include(c => c.Category).FirstOrDefault(x => x.CommodityId == id);
                    if (commodity != null)
                    {
                        commodityDTO.CommodityId = commodity.CommodityId;
                        commodityDTO.CommodityName = commodity.CommodityName;
                        commodityDTO.UnitPrice = commodity.UnitPrice ?? -1;
                        commodityDTO.UnitInStock = commodity.UnitInStock ?? -1;
                        commodityDTO.CategoryName = commodity.CommodityName;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commodityDTO;
        }
        //get commodity by name (func filter)
        public static List<CommodityDTO> GetCommodityDTOByName(string commodityName)
        {
            List<CommodityDTO> commodityDTO = new List<CommodityDTO>();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    commodityDTO = context.Commodities.Include(c => c.Category)
                        .Where(x => x.CommodityName.Contains(commodityName))
                        .Select(x => new CommodityDTO { CommodityId = x.CommodityId, CommodityName = x.CommodityName, UnitPrice = x.UnitPrice, UnitInStock = x.UnitInStock, CategoryName = x.Category.CategoryName }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commodityDTO;
        }
        public static List<CommodityDTO> GetCommodityDTOByCategoryId(int categroryName)
        {
            List<CommodityDTO> commodityDTO = new List<CommodityDTO>();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    commodityDTO = context.Commodities.Include(c => c.Category)
                        .Where(x => x.Category.CategoryId == categroryName)
                        .Select(x => new CommodityDTO { CommodityId = x.CommodityId, CommodityName = x.CommodityName, UnitPrice = x.UnitPrice, UnitInStock = x.UnitInStock, CategoryName = x.Category.CategoryName }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commodityDTO;
        }
    }
}
