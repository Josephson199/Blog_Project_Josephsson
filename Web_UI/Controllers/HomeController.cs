using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DataStore;
using Web_UI.Models.Home;
using Infrastructure.Parsers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Web_UI.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;
        private readonly BlogDataStore _dataStore;
        const string StorageFolder = "BlogFiles";       

        public HomeController(BlogDataStore dataStore, IMemoryCache cache)
        {           
            _cache = cache;
            _dataStore = dataStore;
        }

        public IActionResult About()
        {    
            var viewModel = new AboutViewModel
            {
               
            };
            return View(viewModel);
        }

        public IActionResult Work()
        {
            return View();
        }

      

        public IActionResult Blog()
        {
            var posts = _dataStore.GetAllPosts().Where(p => !p.IsDeleted && p.IsPublic).ToList();

            var viewModel = new BlogViewModel
            {
                PostModels = new List<PostModel>()
            };

            if (!posts.Any())
            {
                return View(viewModel);               
            }          

            foreach (var post in posts)
            {
                var postModel = new PostModel
                {
                    PubDate = post.PubDate,
                    Body = post.Body,
                    OutputStrategy = new MarkdigParser(),
                    Title = post.Title
                };

                viewModel.PostModels.Add(postModel);
            }

            return View(viewModel);
        }

        public IActionResult Login()
        {
            string result = "";
            Request.Cookies.TryGetValue("LoggedIn", out result);

            if(result == "LoggedIn")
            {
                return RedirectToAction("About");
            }

            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            };

            if(model.Email.ToLower().Trim() == Environment.GetEnvironmentVariable("ADMIN_USERNAME") && model.Password.Trim() == Environment.GetEnvironmentVariable("ADMIN_PASSWORD"))
            {
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(999)
                };
                
                Response.Cookies.Delete("LoggedIn");

                Response.Cookies.Append("LoggedIn", "LoggedIn", options);

                return RedirectToAction("About");
            }

            return View(model);

        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("LoggedIn");
            return RedirectToAction("About");
        }

    }
}