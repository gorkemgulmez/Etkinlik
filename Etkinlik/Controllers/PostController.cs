using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.Controllers
{
    [Route("post")]
    public class PostController : Controller
    {
        #region
        protected ApplicationDbContext mContext;
        #endregion

        public PostController(ApplicationDbContext context) {
            mContext = context;
        }

        // GET: /<controller>/
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        [Route("post")]
        public IActionResult PostDetail(PostModel post) {
            if(post.PostName != null && post.PostName.Length > 0 && post.PostDesc.Length > 0)
            {
                
                post.PostCreateTime = DateTime.Now;
                //Spost.UserModel = 
                mContext.Add(post);
                mContext.SaveChanges();
                
            }

            return View();
        }

        /*
         * var oldPost = mContext.Posts.Where(pst => pst.Id == post.Id).FirstOrDefault();
            if (post.PostName != null && !post.PostName.Equals("")) oldPost.PostName = post.PostName;
            if (post.PostDesc != null && !post.PostDesc.Equals("")) oldPost.PostDesc = post.PostDesc;
            */

        [HttpGet]
        [Route("post")]
        public IActionResult PostDetail() {
            return View();
        }
    }
}
