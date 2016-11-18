using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class NewLikeModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int  CommentId { get; set; }
    }
}