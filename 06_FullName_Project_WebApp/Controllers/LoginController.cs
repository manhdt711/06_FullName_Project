using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;
using ElecStore.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _06_FullName_Project_WebApp.Controllers
{
    public class LoginController : Controller
{
        private readonly HttpClient client = null;
        private string url = "";
        public LoginController()
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
        public async Task<IActionResult> Login(ElecStore.Models.User user)
        {
            HttpResponseMessage respone = await client.PostAsJsonAsync($"{url}/Login",user);
            string strData = await respone.Content.ReadAsStringAsync();
            ElecStore.Models.User userLogin = JsonConvert.DeserializeObject<ElecStore.Models.User>(strData);
            HttpContext.Session.SetInt32("userId",userLogin.UserId);
            return View();
        }
    }
}
