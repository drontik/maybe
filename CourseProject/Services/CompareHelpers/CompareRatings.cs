using System.Collections.Generic;
using CourseProject.Domain.Entities;

namespace CourseProject.Services
{
    public class CompareRatings : IComparer<Rating>
    {
        public int Compare(Rating x, Rating y)
        {
            return x.Value - y.Value;
        }
    }
}