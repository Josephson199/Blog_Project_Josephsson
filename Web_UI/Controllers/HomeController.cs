using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataStore;
using Web_UI.Models.Home;
using DataStore.Models;
using Infrastructure.Parsers;

namespace Web_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDataStore _dataStore;
        const string StorageFolder = "BlogFiles";       

        public HomeController(BlogDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IActionResult About()
        {    
            var viewModel = new AboutViewModel
            {
               
            };
            return View(viewModel);
        }

        public IActionResult Contact()
        {
            return View();
        }

      

        public IActionResult Blog()
        {
            var posts = _dataStore.GetAllPosts();

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

       
    }
}