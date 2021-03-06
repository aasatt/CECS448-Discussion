using Discussions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Markdig;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Discussions.Controllers
{
    public class TopicController : Controller
    {
        private System.Guid topicId = new System.Guid("64B84A04-7C9E-4355-BBB6-83B203341420");
        private DiscussionContext _context;

        private User currentUser = new User {
            id= new System.Guid("554FD732-7736-4B6C-AA40-2A1175733C2E"),
            name = "Demo User", 
            imageUrl = "https://randomuser.me/api/portraits/men/2.jpg"
        };
        // private User currentUser = new User {
        //     id= new System.Guid("92462AD2-1166-4638-A667-45612901D5BF"),
        //     name = "mary-l", 
        //     imageUrl = "https://randomuser.me/api/portraits/women/19.jpg"
        // };
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
                id = System.Guid.NewGuid(),
                author = currentUser,
                message = formattedComment,
                rawComment = comment
            };
            topic.comments.Add(newComment);
            try
            {
                _context.Topic.Update(topic);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var redirectUrl = "/";
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(System.Guid? commentId, string message)
        {
            if (commentId == null)
            {
                return NotFound();
            }
            var comment = await _context.Comment.FindAsync(commentId);
            comment.rawComment = message;
            comment.message = Markdown.ToHtml(message);
            try
            {
                _context.Comment.Update(comment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var redirectUrl = "/";
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(System.Guid? commentId)
        {
            if (commentId == null)
            {
                return NotFound();
            }
            var comment = await _context.Comment.FindAsync(commentId);
            try
            {
                _context.Comment.Remove(comment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var redirectUrl = "/";
            return Json(new { Url = redirectUrl });
        }


        public string ToHTML(string markdown)
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
