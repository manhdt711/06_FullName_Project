using DocumentFormat.OpenXml.Spreadsheet;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace _06_FullName_Project_WebApp.Controllers
{
    public class PromotionController : Controller
{
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string url = "";
        public PromotionController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            url = "https://localhost:7194/api/Promotion";
        }
        public IActionResult Index()
    {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (HttpContext.Session.GetInt32("UserId") != 0)
            {
                return Redirect("login");
            }
            else {
                return View();
            }
         
    }
        public async Task<IActionResult> CreatePromote(string codePromo, double valuePromo)
        {
            Promotion promotion = new Promotion() { 
            PromotionName = codePromo,
                Discount = valuePromo
            };

            HttpResponseMessage respone = await client.PostAsJsonAsync($"{url}/Create", promotion);
            string strData = await respone.Content.ReadAsStringAsync();
            return Redirect("/promotion");
        }
    }
}
