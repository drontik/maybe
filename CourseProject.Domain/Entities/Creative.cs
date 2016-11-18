using System;
using System.Collections.Generic;

namespace CourseProject.Domain.Entities
{
    public class Creative
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual CreativeCategory Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
    }
}
