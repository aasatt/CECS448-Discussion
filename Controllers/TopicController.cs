using Discussions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Markdig;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Discussions.Controllers
{
    public class TopicController : Controller
    {
        private System.Guid topicId = new System.Guid("64B84A04-7C9E-4355-BBB6-83B203341420");
        private DiscussionContext _context;

        private User currentUser = new User {
            id= new System.Guid("667F39D4-9CE0-4BE7-954C-FD2D04CDD38B"),
            name = "Demo User", 
            imageUrl = "https://randomuser.me/api/portraits/men/2.jpg"
        };
        public TopicController(DiscussionContext context) {
            _context = context;
        }

        public IActionResult Index()
        {
            Topic t = _context.Topic.Find(topicId);
            List<Comment> comments = _context.Comment.Include("author").ToList();
            t.comments = comments;  
            return View(t);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(System.Guid? id, string comment)
        {
            if (id == null)
            {
                return NotFound();
            }
            var topic = await _context.Topic.FindAsync(id);
            List<Comment> comments = _context.Comment.Include("author").ToList();
            topic.comments = comments;  
            if (topic == null)
            {
                return NotFound();
            }
            var formattedComment = Markdown.ToHtml(comment);
            Comment newComment = new Comment {
                id = new System.Guid(),
                author = currentUser,
                message = formattedComment
            };
            topic.comments.Add(newComment);
            try
            {
                _context.Topic.Update(topic);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var redirectUrl = "/topic";
            return Json(new { Url = redirectUrl });
        }


        public string Welcome(string markdown)
        {
            if (markdown == null) {
                return "";
            }
            var x = Markdig.Markdown.ToHtml(markdown);
            if (x == null) {
                return "Preview will show here...";
            }
            return x;
        }
 
    }
}
