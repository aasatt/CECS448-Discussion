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


namespace Discussions.Controllers
{
    public class TopicController : Controller
    {
        private System.Guid topicId = new System.Guid("64B84A04-7C9E-4355-BBB6-83B203341420");
        private DiscussionContext _context;
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
    }
}
