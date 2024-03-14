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
            url = "https://localhost:7194/api/User";
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
