using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etkinlik.Data;
using Etkinlik.Models;
using Etkinlik.Models.ActivityViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.Controllers
{
    [Route("Post")]
    public class PostController : Controller
    {
        #region
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        //constructor
        public PostController(ApplicationDbContext context,
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _applicationDbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Post Controller (add update and delete activity)
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [Authorize]
        public IActionResult AddActivity(UpdateActivityModel post)
        {
            if(post == null )
            {
                return View();
            }

            var user = _applicationDbContext.Users.FirstOrDefault(e => e.Id == _userManager.GetUserId(HttpContext.User));

            var newPost = new PostModel {
                PostName = post.PostName,
                PostDesc = post.PostDesc,
                PostTime = post.PostTime,
                PostCreateTime = DateTime.Now,
                ApplicationUser = user
            };

            _applicationDbContext.Posts.AddAsync(newPost);
            _applicationDbContext.SaveChanges();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddActivity()
        {
            return View("AddActivity", new UpdateActivityModel());
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public IActionResult UpdateActivity(UpdateActivityModel post, int? id)
        {
            PostModel updPost = _applicationDbContext.Posts.First(p => p.Id == id);
            if (updPost == null)
                return View("Index");

            if (!_userManager.GetUserId(User).Equals(updPost.ApplicationUser.Id))
                return View("Index");

            if (!post.PostName.Equals("") && !post.PostName.Equals(updPost.PostName))
                updPost.PostName = post.PostName;
            if (!post.PostDesc.Equals(updPost.PostName))
                updPost.PostDesc = post.PostDesc;
            if (!(post.PostTime == null) && !post.PostTime.Equals(updPost.PostTime))
                updPost.PostTime = post.PostTime;

            _applicationDbContext.Posts.Update(updPost);
            _applicationDbContext.SaveChanges();
            return View();
        }

        [HttpGet]
        [Route("update/{id:int}")]
        public IActionResult UpdateActivity(int Id)
        {
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                return RedirectToAction("Index");
            }
            PostModel pModel= _applicationDbContext.Posts.First(p => p.Id == Id);
            if (pModel == null)
                return RedirectToAction("Index");

            UpdateActivityModel updateActivityModel = new UpdateActivityModel
            {
                Id = pModel.Id,
                PostName = pModel.PostName,
                PostDesc = pModel.PostDesc,
                PostTime = pModel.PostTime
            };
            
            return View("UpdateActivity", updateActivityModel);
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public IActionResult DeleteActivity(UpdateActivityModel post, int? id)
        {
            var delPost = _applicationDbContext.Posts.First(p => p.Id == id);
            if(delPost == null)
                return View("Index");

            if (!delPost.ApplicationUser.Id.Equals(_userManager.GetUserId(User)))
                return View("Index");                    

            _applicationDbContext.Remove(delPost);
            _applicationDbContext.SaveChanges();
            return View();
        }
/*
        /// <summary>
        /// PostUser methods (join or quit activity)
        /// </summary>
        /// <param name="User"></param>
        [Authorize]
        public void AddUser(ApplicationUser User)
        {
            //if not logged in -> log in redirect
            //if user id and post id didnt match add post 
            //else u are already in that activity message
        }

        [Authorize]
        public void DeleteUser()
        {
            //if not logged in -> log in redirect
            //if user id and post id did match add post 
            //else u are not in that activity message
        }*/
    }
}
