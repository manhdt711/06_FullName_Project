using ClassLibrary2.DTO;
using DocumentFormat.OpenXml.Drawing.Charts;
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
            
            //ViewBag.isAdmin = HttpContext.Session.GetInt32("isAdmin");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View();
        }
        public async Task<IActionResult> DeleteOrderOrderChangeStatus(int orderId, string typeAction)
        {
            HttpResponseMessage respone;
            ElecStore.Models.Order order = new ElecStore.Models.Order();
            order.OrderId = orderId;
            if (typeAction.Equals("delete"))
            {
                order.PaymentMethod = "cancel";
                respone = await client.PostAsJsonAsync($"{url}/ChangeStatusOrder", order);

            }
            else if (typeAction.Equals("changeStatusShipping"))
            {
                order.PaymentMethod = "shipping";
                respone = await client.PostAsJsonAsync($"{url}/ChangeStatusOrder", order);

            }
            else
            {
                order.PaymentMethod = "done";
                respone = await client.PostAsJsonAsync($"{url}/ChangeStatusOrder", order);

            }
            string strData = await respone.Content.ReadAsStringAsync();
            return Redirect("/order");
        }
    }
}
