using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class NewRatingModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1,5)]
        public int Value { get; set; }

        [Required]
        public int CreativeId { get; set; }

        [Required]
        public string  UserName { get; set; }

    }
}