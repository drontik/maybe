using System;

namespace CourseProject.Domain.Entities
{
    public class Chapter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int  Number { get; set; }

        public string  Body { get; set; }

        public int CreativeId { get; set; }

        public DateTime Created { get; set; }
      
    }
}