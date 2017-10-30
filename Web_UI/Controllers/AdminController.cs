using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataStore;
using Web_UI.Models.New;
using DataStore.Models;

namespace Web_UI.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly BlogDataStore _dataStore;

        public AdminController(BlogDataStore blogDataStore)
        {
            _dataStore = blogDataStore;
        }

        [Route("new")]
        public IActionResult New()
        {
            var viewModel = new NewViewModel
            {
                NewPost = new NewPost()
            };

            return View(viewModel);
        }


        [Route("new/save")]
        [HttpPost]        
        public IActionResult New(NewViewModel viewModel, string submitButton)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            var post = new BlogPost
            {
                Title = viewModel.NewPost.Title,
                Body = viewModel.NewPost.Body,
                LastModified = DateTimeOffset.Now,
                IsDeleted = false,
            };

            if (submitButton == "Publish")
            {
                post.PubDate = DateTimeOffset.Now;
                post.IsPublic = true;
            }

            if (Request != null)
            {
                _dataStore.SaveFiles(Request.Form.Files.ToList());
            }

            _dataStore.SavePost(post);

            if(submitButton == "Save Draft")
            {
                return RedirectToAction("Draft", "Admin");
            };

            return RedirectToAction("Blog", "Home");
        }


    }
}