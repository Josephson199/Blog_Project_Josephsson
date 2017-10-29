using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models.Home
{
    public class IndexViewModel
    {
        public IEnumerable<PostSummaryModel> PostSummaries { get; set; }
    }
}
