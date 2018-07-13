using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etkinlik.Data;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.Controllers
{
    [Route("post")]
    public class PostController : Controller
    {
        #region
        protected ApplicationDbContext mContext;
        #endregion

        public PostController(ApplicationDbContext context)
        {
            mContext = context;
        }

        [HttpPost]
        [Route("post")]
        public IActionResult AddActivity(PostModel post)
        {
            if(post == null)
            {
                return View();
            }

            var newPost = new PostModel{
                PostName = post.PostName,
                PostDesc = post.PostDesc,
                PostCreateTime = DateTime.Now,
                PostTime = post.PostTime
            };

            mContext.Posts.AddAsync(newPost);
            return View();
        }

        [HttpGet]
        [Route("post")]
        public IActionResult AddActivity()
        {
            return View("AddActivity", new PostModel());
        }
    
    }
}
