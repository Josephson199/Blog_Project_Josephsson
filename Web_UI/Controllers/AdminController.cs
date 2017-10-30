using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataStore;
using Web_UI.Models.New;
using DataStore.Models;
using Web_UI.Models.Admin;

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



        [Route("drafts")]
        public IActionResult Drafts()
        {
            var postModels = _dataStore.GetAllDrafts().Where(post => !post.IsPublic);

            var viewModel = new DraftsViewModel
            {
                DraftSummaries = new List<DraftSummaryModel>()
            };

            if(!postModels.Any())
            {
                return View(viewModel);
            }

            foreach (var post in postModels)
            {
                viewModel.DraftSummaries.Add(new DraftSummaryModel
                {
                    Id = post.Id,
                    CommentCount = post.Comments.Count,
                    PublishTime = post.PubDate,
                    Title = post.Title
                });
            }   
            
            return View(viewModel);
        }

        [Route("Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var post = _dataStore.GetPost(id);

            if(post == null)
            {
                return RedirectToAction("Drafts");
            }
         
            var viewModel = new EditViewModel
            {
                EditedPostModel = new EditedPostModel
                {
                    Id = post.Id.ToString(),
                    Title = post.Title,
                    Body = post.Body
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel viewModel, string submitButton)
        {           
            var post = _dataStore.GetPost(viewModel.EditedPostModel.Id);

            if (post == null)
            {
                return RedirectToAction("Drafts");
            }           

            return View(viewModel);
        }

    }
}