using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class NewCategoryModel
    {
        [Required]
        public int  Id { get; set; }

        [Required]
        public string  Name { get; set; }
    }
}