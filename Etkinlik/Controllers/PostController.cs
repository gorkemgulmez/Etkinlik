using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etkinlik.Data;
using Etkinlik.Models;
using Etkinlik.Models.ActivityViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.Controllers
{
    [Route("Post")]
    [Authorize]
    public class PostController : Controller
    {
        #region private-readonly
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
        [HttpGet]
        public IActionResult Index()
        {
            var postList = _applicationDbContext.Posts.Where(p=> p.ApplicationUserId == _userManager.GetUserId(HttpContext.User)).ToList();
            return View("Index", postList);
        }

        [HttpGet("add")]
        public IActionResult AddActivity()
        {
            return View("AddActivity", new UpdateActivityModel());
        }

        [HttpGet("update/{id}")]
        public IActionResult UpdateActivity(int id)
        {
            /*if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                return Redirect("/Account/Login");
            }*/

            PostModel pModel;
            try
            {
                pModel = _applicationDbContext.Posts.First(p => p.Id == id);
            }
            catch (Exception)
            {
                return Redirect("/");
            }

            if (!pModel.ApplicationUserId.Equals(_userManager.GetUserId(HttpContext.User)))
                return Redirect("/");

            UpdateActivityModel updateActivityModel = new UpdateActivityModel
            {
                Id = pModel.Id,
                PostName = pModel.PostName,
                PostDesc = pModel.PostDesc,
                PostDate = pModel.PostDate,
                PostTime = pModel.PostTime
            };

            return View("UpdateActivity", updateActivityModel);
        }

        [HttpPost("add")]
        public IActionResult AddActivity(UpdateActivityModel post)
        {
            if (post == null)
            {
                //Mesaj gönder
                return View();
            }
            
            var user = _applicationDbContext.Users.FirstOrDefault(e => e.Id == _userManager.GetUserId(HttpContext.User));

            if (post.PostName == null || post.PostDesc == null || post.PostDate == new DateTime())
            {
                return Redirect("/");
            }

            var newPost = new PostModel
            {
                PostName = post.PostName,
                PostDesc = post.PostDesc,
                PostDate = post.PostDate,
                PostTime = post.PostTime,
                PostCreateTime = DateTime.Now,
                ApplicationUser = user,
                ApplicationUserId = user.Id
            };

            _applicationDbContext.Posts.AddAsync(newPost);
            _applicationDbContext.SaveChanges();
            //AddUser
            AddUser(newPost.Id);
            return Redirect("/");
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateActivity(int id, UpdateActivityModel post)
        {
            PostModel updPost = _applicationDbContext.Posts.First(p => p.Id == id);
            if (updPost == null)
                return Redirect("/");

            if (!_userManager.GetUserId(User).Equals(updPost.ApplicationUserId))
                return Redirect("/");

            if (!post.PostName.Equals("") && !post.PostName.Equals(updPost.PostName))
                updPost.PostName = post.PostName;
            if (!post.PostDesc.Equals(updPost.PostName))
                updPost.PostDesc = post.PostDesc;
            if (post.PostDate != null && post.PostDate != new DateTime() && !post.PostDate.Equals(updPost.PostDate))
                updPost.PostDate = post.PostDate;
            if (!(post.PostTime == null) && !post.PostTime.Equals(updPost.PostTime))
                updPost.PostTime = post.PostTime;

            _applicationDbContext.Posts.Update(updPost);
            _applicationDbContext.SaveChanges();
            return Redirect("/");
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteActivity(int id)
        {
            ApplicationUser user = _applicationDbContext.Users.First(u => u.Id == _userManager.GetUserId(HttpContext.User));
            PostModel delPost;
            try
            {
                delPost = _applicationDbContext.Posts.First(p => p.Id == id);
            }
            catch (Exception)
            {
                return Redirect("/");
            }

            try
            {
                if (delPost.ApplicationUserId != user.Id)
                    return Redirect("/");
            }
            catch (Exception)
            {
                return Redirect("/");
            }
            _applicationDbContext.Remove(delPost);
            _applicationDbContext.SaveChanges();
            return Redirect("/Post/add");
        }

        public List<string> getUsers(PostModel post)
        {
            List<UserPostModel> users = _applicationDbContext.UserPosts.Where(up => up.PostModelId == post.Id).ToList();
            List<string> userName = new List<string>();
            foreach (var model in users)
            {
                userName.Add(_applicationDbContext.Users.First(u => u.Id == model.ApplicationUserId).FullName);
            }
            return userName;
        }

        /// PostUser methods (join or quit activity)
        /// </summary>
        /// <param name="User"></param>
        [HttpGet("join/{id}")]
        public IActionResult AddUser(int id)
        {
            var user = _applicationDbContext.Users.First(u => u.Id == _userManager.GetUserId(HttpContext.User));
            PostModel post;
            try
            {
                post = _applicationDbContext.Posts.First(p => p.Id == id);
               
            }
            catch (Exception)
            {
                return Redirect("/");
            }

            try
            {
                UserPostModel userPost = _applicationDbContext.UserPosts.First(d =>
                         d.ApplicationUserId == user.Id && d.PostModelId == id);
            }
            catch (Exception)
            {

                _applicationDbContext.UserPosts.Add(new UserPostModel
                {
                    ApplicationUserId = user.Id,
                    PostModelId = id
                });
                _applicationDbContext.SaveChanges();
                return Redirect("/");
            }

            return Redirect("/");
        }

        [HttpGet("quit/{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (!_signInManager.IsSignedIn(HttpContext.User))
                return Redirect("/");
            var user = _applicationDbContext.Users.First(u => u.Id == _userManager.GetUserId(HttpContext.User));
            PostModel post;
            try {
                post = _applicationDbContext.Posts.First(p => p.Id == id);
            }
            catch(Exception)
            {
                return Redirect("/");
            }

            UserPostModel userPost;
            try
            {
                userPost = _applicationDbContext.UserPosts.First(d =>
                             d.ApplicationUserId == user.Id && d.PostModelId == id);
            }
            catch (Exception)
            {
                return Redirect("/");
            }

            _applicationDbContext.UserPosts.Remove(userPost);
            _applicationDbContext.SaveChanges();
            return Redirect("/");
        }

        public bool HasIn(PostModel post, string userId)
        {
            try
            {
                var user = _applicationDbContext.Users.First(u => u.Id.Equals(userId));
                
                UserPostModel userPost = _applicationDbContext.UserPosts.First(d =>
                         d.ApplicationUserId == user.Id && d.PostModelId == post.Id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
