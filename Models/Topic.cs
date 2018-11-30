using System;

namespace Discussions.Models {
    public class Topic {
        public int id = 1;
        public string title = "Test";
        public string message = "This is a test post.";

        public string[] comments;
    }

}