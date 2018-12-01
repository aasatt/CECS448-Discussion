using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Markdig;
using System.Collections.Generic;


namespace Discussions.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DiscussionContext(
                serviceProvider.GetRequiredService<DbContextOptions<DiscussionContext>>()))
            {
                // Look for any movies.
                if (context.Topic.Any())
                {
                    return;   // DB has been seeded
                }
                string s = "There is an English expression  **do for** , which means  *to kill, to execute, to ruin, to defeat etc.*  and this expression seems to always be used in passive voice: e.g.)  **We are done for**.\r\n\r\nI understand this is like an idiom, but why is the preposition  **for**  used? Most prepositions have so many meanings to them, and I would like to know what  *for*  in this case means.\r\n\r\n**Edit:** [https://dictionary.cambridge.org/us/dictionary/english/forhttps://en.oxforddictionaries.com/definition/for](https://dictionary.cambridge.org/us/dictionary/english/forhttps://en.oxforddictionaries.com/definition/for)\r\n\r\nIn the definitions of the above links, which  **for**  do you all think the  **for**  in  *done for*  is the closest to?\r\n\r\n**Edit 2:** I don't think the  *for*  is dangling, but I just want to know why  *for*  is used. Like,  *for*  normally means  *purpose* ,  *cause* , or  *to be given* , etc. But the  *for*  in question means quite opposite.";

                User a = new User {
                    id = new Guid(),
                    name = "chris-g",
                    imageUrl = "https://randomuser.me/api/portraits/men/85.jpg"
                };
                Comment c = new Comment {
                    id = new Guid(),
                    message = Markdown.ToHtml(s),
                    author = a
                };
                List<Comment> comments = new List<Comment>();
                comments.Add(c);

                context.Topic.AddRange(
                     new Topic
                     {
                         id = new Guid(),
                         title = "What does 'for' mean in 'We are done for'?",
                         comments = comments,
                     }
                );
                context.SaveChanges();
            }
        }
    }
}