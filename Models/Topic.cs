using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discussions.Models {
    public class Topic {
        [Key]
        public Guid id { get; set; }
        public string title { get; set; }
        public List<Comment> comments { get; set; }
    }

}