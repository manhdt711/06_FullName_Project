using ClassLibrary2.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace _06_FullName_Project_WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string url = "";
        public OrderController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            url = "https://localhost:7194/api/Orders";
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            ElecStore.Models.Order order = new ElecStore.Models.Order();
            order.OrderId = orderId;
            HttpResponseMessage respone = await client.PostAsJsonAsync($"{url}/DeleteOrder", order);
            string strData = await respone.Content.ReadAsStringAsync();
            ElecStore.Models.User userLogin = JsonConvert.DeserializeObject<ElecStore.Models.User>(strData);
            //HttpContext.Session.SetInt32("userId", userLogin.UserId);
            //demo 
            return View("/order");
        }
    }
}
