using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}