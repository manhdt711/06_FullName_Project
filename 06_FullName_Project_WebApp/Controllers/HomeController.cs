using _06_FullName_Project_WebApp.Models;
using ClassLibrary2.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace _06_FullName_Project_WebApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string url = "";
        private string urlPromo = "";
        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            url = "https://localhost:7194/api/Orders";
            urlPromo = "https://localhost:7194/api/Promotion";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Order(string commodityName, int quantity, int price, string customerName, string address, string phoneNumber, string note, int commodityId, string promo)
        {
            ElecStore.Models.Promotion pr = new ElecStore.Models.Promotion();
            if (promo != null)
            {
                HttpResponseMessage respone1 = await client.GetAsync($"{urlPromo}/GetByCode/${promo}");
                string strData1 = await respone1.Content.ReadAsStringAsync();
                pr = JsonConvert.DeserializeObject<ElecStore.Models.Promotion>(strData1);
            }
            OrderDTO orderDTO = new OrderDTO()
            {
                commodityId = commodityId,
                commodityName = commodityName,
                quantity = quantity,
                price = price,
                promotionId = pr == null ? null : pr.PromotionId,
                customerName = customerName,
                address = address,
                phoneNumber = phoneNumber,
                note = note,
                userId = HttpContext.Session.GetInt32("UserId")
            };
            HttpResponseMessage respone = await client.PostAsJsonAsync($"{url}/AddOrder", orderDTO);
            string strData = await respone.Content.ReadAsStringAsync();
            ElecStore.Models.User userLogin = JsonConvert.DeserializeObject<ElecStore.Models.User>(strData);
            //HttpContext.Session.SetInt32("userId", userLogin.UserId);
            //demo 


            return RedirectToAction(nameof(Index));
        }
    }
}