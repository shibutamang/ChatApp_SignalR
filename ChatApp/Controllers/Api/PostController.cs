using ChatApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ChatApp.Controllers.Api
{
    [RoutePrefix("api/Post")]
    public class PostController : ApiController
    {
        [HttpPost]
        [Route("Like")]
        public IHttpActionResult Like([FromBody] int postId)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(userId);

            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                try
                {
                    var result = _context.PostLike.Where(p => p.PostId == postId && p.UserId == userId).FirstOrDefault();
                    var post = _context.Post.Where(p => p.PostId == postId).FirstOrDefault();

                    if (result != null)
                    {
                        _context.PostLike.Remove(result);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.PostLike.Add(new PostLike() { PostId = postId, UserId = userId, Date = DateTime.Now });
                        _context.SaveChanges();

                        if (userId != post.PostedBy)
                        {
                            var notification = new Notification() { Type = "LIKE", Title = currentUser.UserName + " likes your post", Details = currentUser.UserName + " likes your post", LinkUrl = "~/Home/Index", SentDate = DateTime.Now, SentTo = post.PostedBy, IsRead = false, IsDeleted = false, IsReminder = false, Code = "LIKE" };

                            _context.Notification.Add(notification);
                            _context.SaveChanges();

                            //notify respective user
                            NotificationManager.NotifyUser(notification);
                        }
                       
                    }
                }
                catch(Exception e)
                {
                    return BadRequest(e.Message);
                }
               
            }

            return Ok("Updated");
             
        }

    }
}
