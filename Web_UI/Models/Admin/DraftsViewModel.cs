using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models.Admin
{
    public class DraftsViewModel
    {
        public List<DraftSummaryModel> DraftSummaries { get; set; }
    }

    public class DraftSummaryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset PublishTime { get; set; }
        public int CommentCount { get; set; }
    }
}
