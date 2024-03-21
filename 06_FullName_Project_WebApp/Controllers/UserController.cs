using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;
using ElecStore.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _06_FullName_Project_WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string url = "";
        public UserController()
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
        public async Task<IActionResult> Details()
        {
            return View();
        }
    }
}
