using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class RememberChapterModel
    {
        [Required]
        public int ChapterId { get; set; }

        [Required]
        public int CreativeId { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}