using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class NewChapterModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int CreativeId { get; set; }
   
        public string CreatedOn { get; set; }

        public bool Edit { get; set; }
    }
}