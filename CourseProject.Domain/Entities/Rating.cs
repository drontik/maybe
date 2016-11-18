namespace CourseProject.Domain.Entities
{
    public class Rating 
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int CreativeId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
