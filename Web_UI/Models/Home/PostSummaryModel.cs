using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models.Home
{
    public class PostSummaryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset PublishTime { get; set; }
        public int CommentCount { get; set; }
    }
}
