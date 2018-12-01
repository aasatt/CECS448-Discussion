using System;
using System.ComponentModel.DataAnnotations;

namespace Discussions.Models {
    public class User {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
    }
}