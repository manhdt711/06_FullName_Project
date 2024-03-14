using ClassLibrary2.DTO;
using ElecStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.ViewModel
{
    public class CommodityCategoryDAO
    {
        public static List<CommodityCategory> GetCommodityCategory()
        {
            List<CommodityCategory> commodityCategory = new List<CommodityCategory>();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    commodityCategory = context.CommodityCategories.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commodityCategory;
        }
    }
}
