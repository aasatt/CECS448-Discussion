using Discussions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Markdig;

namespace Discussions.Controllers
{
    public class TopicController : Controller
    {
        // 
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            ViewData["Message"] = Markdown.ToHtml("There is an English expression  **do for** , which means  *to kill, to execute, to ruin, to defeat etc.*  and this expression seems to always be used in passive voice: e.g.)  **We are done for**.\r\n\r\nI understand this is like an idiom, but why is the preposition  **for**  used? Most prepositions have so many meanings to them, and I would like to know what  *for*  in this case means.\r\n\r\n**Edit:** [https://dictionary.cambridge.org/us/dictionary/english/forhttps://en.oxforddictionaries.com/definition/for](https://dictionary.cambridge.org/us/dictionary/english/forhttps://en.oxforddictionaries.com/definition/for)\r\n\r\nIn the definitions of the above links, which  **for**  do you all think the  **for**  in  *done for*  is the closest to?\r\n\r\n**Edit 2:** I don't think the  *for*  is dangling, but I just want to know why  *for*  is used. Like,  *for*  normally means  *purpose* ,  *cause* , or  *to be given* , etc. But the  *for*  in question means quite opposite.");
            return View();
        }
    }

}
