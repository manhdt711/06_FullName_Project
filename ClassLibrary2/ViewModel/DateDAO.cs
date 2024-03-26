using ClassLibrary2.DTO;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.ViewModel
{
    public class DateDAO
    {
        public static DateDTO GetDateByOrderID(int orderID)
        {
            DateDTO dateDTOs = new DateDTO();
            try
            {
                using (var context = new ElectricStore1Context())
                {
                    var temp = context.Orders.Include(x => x.Date).Where(y => y.OrderId == orderID).FirstOrDefault();
                    if (temp == null)
                    { }
                    else
                    {
                        dateDTOs.DateID = temp.DateId ?? -1;
                        dateDTOs.OrderDate = new DateTime((int)temp.Date.Year, (int)temp.Date.Month, (int)temp.Date.Day);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dateDTOs;
        }
    }
}
