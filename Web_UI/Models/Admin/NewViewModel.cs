using System.ComponentModel.DataAnnotations;

namespace Web_UI.Models.New
{
    public class NewViewModel
    {
        public NewPost NewPost { get; set; }
    }

    public class NewPost
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
