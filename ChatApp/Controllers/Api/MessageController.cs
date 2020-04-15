using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatApp.Controllers.Api
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        [HttpGet]
        [Route("GetMessage")]
        public IHttpActionResult GetMessage([FromUri]string UserName = "")
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var currentUser = User.Identity.Name;
                var users = _context.ChatMessages.AsNoTracking().AsQueryable()
                    .Where(m => (m.SentTo == UserName && m.SentFrom == currentUser) || (m.SentTo == currentUser && m.SentFrom == UserName)).ToList();
                return Ok(users);

            }
             
        }
    }
}
