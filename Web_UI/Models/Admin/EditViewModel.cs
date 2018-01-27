using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models.Admin
{
    public class EditViewModel
    {
        public EditedPostModel EditedPostModel { get; set; }
    }

    public class EditedPostModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublic { get; set; }
    }
}
