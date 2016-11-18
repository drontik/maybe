using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class NewCommentModel
    {
        [Required]
        public int  Id { get; set; }

        [Required]
        public string  Text { get; set; }

        [Required]
        public int  CreativeId { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime PostDate { get; set; }

        public string AvatarUri { get; set; }

        public ICollection<NewLikeModel> Likes { get; set; }

    }
}