using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using ElecStore.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using ClassLibrary2.ViewModel;
using OfficeOpenXml;
using ClassLibrary2.DTO;

namespace eStoreClient.Controllers
{
    public class CommodityController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiCateUrl = "";

        public CommodityController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiCateUrl = "https://localhost:7194/api/Commodity";
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
/*            HttpResponseMessage respone = await client.GetAsync($"{ApiCateUrl}/GetCommodityManager");
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Commodity> listCommoditys = JsonSerializer.Deserialize<List<Commodity>>(strData, options);*/

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Commodity model)
        {

            var jsonProduct = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"{ApiCateUrl}/CreateCommodity", content);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index), "Commodity");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Commodity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CommodityDAO.UpdateCommodity(model);
                }
                catch (Exception ex)
                {
                    return RedirectToAction(nameof(Index), "Commodity");

                }
            }

            return RedirectToAction(nameof(Index), "Commodity");
        }

        public async Task<IActionResult> ExportToExcel()
        {

            HttpResponseMessage respone = await client.GetAsync($"{ApiCateUrl}/GetCommodityManager");
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<ClassLibrary2.DTO.CommodityDTO> listCommodities = JsonSerializer.Deserialize<List<CommodityDTO>>(strData, options);

            // Tạo một ExcelPackage mới
            using (var excelPackage = new ExcelPackage())
            {
                // Tạo một worksheet mới
                var worksheet = excelPackage.Workbook.Worksheets.Add("Commodities");

                // Load dữ liệu từ danh sách vào worksheet
                worksheet.Cells.LoadFromCollection(listCommodities, true);

                // Thêm cột filter vào worksheet
                worksheet.Cells[worksheet.Dimension.Address].AutoFilter = true;

                // Lưu workbook vào một MemoryStream
                var memoryStream = new MemoryStream();
                excelPackage.SaveAs(memoryStream);

                // Đặt vị trí của memoryStream ở đầu
                memoryStream.Position = 0;

                // Trả về file Excel như một file tải về
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Commodities.xlsx");
            }
        }


    }
}
