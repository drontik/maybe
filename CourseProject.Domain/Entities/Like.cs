namespace CourseProject.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}