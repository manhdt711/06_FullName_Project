using ClassLibrary2.DTO;
using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
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
                        .Include(c => c.Category)
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
    }
}
