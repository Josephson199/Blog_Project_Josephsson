using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataStore;
using Web_UI.Models.Home;
using DataStore.Models;

namespace Web_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDataStore _dataStore;
        const string StorageFolder = "BlogFiles";

        public IEnumerable<PostSummaryModel> PostSummaries { get; private set; }

        public HomeController(BlogDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogPost> postModels = _dataStore.GetAllPosts().Where(p => p.IsPublic && !p.IsDeleted);

            PostSummaries = postModels.Select(p => new PostSummaryModel
            {
                Id = p.Id,
                Title = p.Title,
                PublishTime = p.PubDate,
                CommentCount = p.Comments.Where(c => c.IsPublic).Count(),
            });

            var viewModel = new IndexViewModel
            {
                PostSummaries = PostSummaries
            };

            return View(viewModel);
        }
    }
}