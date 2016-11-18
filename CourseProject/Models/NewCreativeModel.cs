using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseProject.Domain.Entities;

namespace CourseProject.Models
{
    public class NewCreativeModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Created { get; set; }

        public double AvgRating { get; set; }

        public string AvatarUri { get; set; }

        public CreativeCategory Category { get; set; }

        public IEnumerable<Chapter> Chapters { get; set; }

        public IEnumerable<NewCommentModel> Comments { get; set; }

        public IEnumerable<Rating> Rating { get; set; }

        public IEnumerable<TagsViewModel> Tags { get; set; } 
    }
}