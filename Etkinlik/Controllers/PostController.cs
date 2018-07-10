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

        public PostController(ApplicationDbContext context)
        {
            mContext = context;
        }

        [HttpPost]
        [Route("post")]
        public IActionResult AddActivity(PostModel post)
        {
            /*if (post.PostName != null && post.PostName.Length > 0 && post.PostDesc.Length >= 0)
            {
                if(!mContext.Users.Any())
                {
                    mContext.Users.Add(new UserModel {
                        UserName = "grkem",
                        FullName = "full",
                        Password = "123",
                        UserEmail = "asdf@asdfjk.co"
                       });
                    mContext.SaveChanges();
                }

                if (post.UserModel == null) post.UserModel = mContext.Users.FirstOrDefault();
                mContext.Add(post);
                mContext.SaveChanges();

            }*/

            return View();
        }

        /*
         * var oldPost = mContext.Posts.Where(pst => pst.Id == post.Id).FirstOrDefault();
            if (post.PostName != null && !post.PostName.Equals("")) oldPost.PostName = post.PostName;
            if (post.PostDesc != null && !post.PostDesc.Equals("")) oldPost.PostDesc = post.PostDesc;
            */

        [HttpGet]
        [Route("post")]
        public IActionResult AddActivity()
        {
            return View("AddActivity", new PostModel());
        }
    }
}
