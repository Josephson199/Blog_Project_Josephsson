using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IBlogPost
    {
        string Title { get; set; }
        string Body { get; set; }
    }
}
