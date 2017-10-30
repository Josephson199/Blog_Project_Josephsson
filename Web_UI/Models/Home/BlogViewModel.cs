using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models.Home
{
    public class BlogViewModel
    {
        public List<PostModel> PostModels { get; set; }
    }

    public class PostModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        
        public IOutputStrategy OutputStrategy { get; set; }
        public string Parse()
        {
            return OutputStrategy.Transform(this.Body);
        }
    }
}
