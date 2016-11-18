using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class SearchViewModel
    {
        [Required]
        [MinLength(3)]
        public string  Pattern { get; set; }

        [Required]
        public bool ChapterName { get; set; }

        [Required]
        public bool ChapterText { get; set; }

        [Required]
        public bool CreativeName { get; set; }

        [Required]
        public bool CreativeDescription { get; set; }

        [Required]
        public bool TagName { get; set; }

        [Required]
        public bool CommentText { get; set; }

        [Required]
        public bool CommentAuthor { get; set; }

        [Required]
        public bool CreativeAuthor { get; set; }
    }
}