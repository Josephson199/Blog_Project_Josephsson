using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models.Admin
{
    public class AllViewModel
    {
        public List<AllSummaryModel> AllSummaries { get; set; }
    }

    public class AllSummaryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset PublishTime { get; set; }
        public int CommentCount { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublic { get; set; }
    }
}
