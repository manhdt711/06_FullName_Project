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
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Commodity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /* var jsonProduct = JsonSerializer.Serialize(model);
                     var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                     HttpResponseMessage response = await client.PutAsync($"{ApiCateUrl}/Update'", content);
                     response.EnsureSuccessStatusCode();
                     return RedirectToAction(nameof(Index), "Home");*/
                    CommodityDAO.UpdateCommodity(model);
                }
                catch (Exception ex)
                {
                    return RedirectToAction(nameof(Index), "Home");

                }
            }

            return RedirectToAction(nameof(Index), "Home");
        }

    }
}
