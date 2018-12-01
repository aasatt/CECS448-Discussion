using System;
using System.ComponentModel.DataAnnotations;

namespace Discussions.Models {
    public class Comment {
        [Key]
        public Guid id { get; set; }
        public string message { get; set; }
        public Discussions.Models.User author { get; set; }
    }
}