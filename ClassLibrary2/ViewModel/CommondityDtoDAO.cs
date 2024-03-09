using ClassLibrary2.DTO;
using ElecStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                        .Join(
                            context.CommodityCategories,
                            commodity => commodity.CategoryId,
                            category => category.CategoryId,
                            (commodity, category) => new CommodityDTO
                            {
                                CommodityId = commodity.CommodityId,
                                CommodityName = commodity.CommodityName,
                                UnitPrice = commodity.UnitPrice ?? 0, // Handling nullable value
                                UnitInStock = commodity.UnitInStock ?? 0, // Handling nullable value
                                CategoryName = category.CategoryName
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
    }
}
