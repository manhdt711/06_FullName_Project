using DocumentFormat.OpenXml.Spreadsheet;
using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.ViewModel
{
    public class CommodityDAO
    {
        public static void SaveCommodity(Commodity commodity)
        {
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    context.Commodities.Add(commodity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCommodityById(int id)
        {
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    var commodityToDelete = context.Commodities.SingleOrDefault(u => u.CommodityId == id);
                    context.Commodities.Remove(commodityToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCommodity(Commodity commodity)
        {
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    context.Entry(commodity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Commodity FindById(int id)
        {
            var commodity = new Commodity();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    commodity = context.Commodities.FirstOrDefault(c => c.CommodityId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return commodity;
        }
    }
}
