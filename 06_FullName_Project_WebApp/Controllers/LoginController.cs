﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string url = "";
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            url = "https://localhost:7194/api/Users";
        }
        public async Task<IActionResult> Index(bool isError)
        {
            if (isError) {
                ViewBag.ErrorLogin = "loginFail";
            }
            return View();
        }
        public async Task<IActionResult> createUser(string newUserName, string newEmail, string newPhoneNumber)
        {
            return View();
        }
        public async Task<IActionResult> Login(ElecStore.Models.User user)
        {
            if (user.UserName.Equals(_configuration["DefaultAccount:UserName"]) && user.Password.Equals(_configuration["DefaultAccount:Password"]))
            {
                // is admin save int = 1 in session
                HttpContext.Session.SetInt32("UserId", 0);
                //return home page
                return Redirect("/home");
            }
            else {
                HttpResponseMessage respone = await client.PostAsJsonAsync($"{url}/Login", user);
                string strData = await respone.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(strData))
                {
                    //login success
                    ElecStore.Models.User userLogin = JsonConvert.DeserializeObject<ElecStore.Models.User>(strData);
                    HttpContext.Session.SetInt32("UserId", userLogin.UserId);
                    return Redirect("/home");
                }
                else {
            
                    return RedirectToAction(nameof(Index), new {isError = true});

                }
                
            }
          
        }
    }
}
