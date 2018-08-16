using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UserModel
{
    public class Comment
    {
        public int ID { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string CommentatorId { get; set; }

        public string CommentatorFullName { get; set; }
    }
}
