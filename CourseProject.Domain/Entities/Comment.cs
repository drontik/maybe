using System;
using System.Collections.Generic;

namespace CourseProject.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int CreativeId { get; set; }

        public string Text { get; set; }

        public DateTime PostDate { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}