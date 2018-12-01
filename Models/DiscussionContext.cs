using Microsoft.EntityFrameworkCore;

namespace Discussions.Models
{
    public class DiscussionContext : DbContext
    {
        public DiscussionContext (DbContextOptions<DiscussionContext> options)
            : base(options)
        {
        }

        public DbSet<Discussions.Models.Topic> Topic { get; set; }
        public DbSet<Discussions.Models.Comment> Comment { get; set; }
        public DbSet<Discussions.Models.User> User { get; set; }

    }
}
