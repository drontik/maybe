using System.Collections.Generic;
using CourseProject.Models;

namespace CourseProject.Services.CompareHelpers
{
    internal class CompareTagViewModels : IEqualityComparer<TagsViewModel>
    {
        public bool Equals(TagsViewModel x, TagsViewModel y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(TagsViewModel obj)
        {
            return obj.GetHashCode();
        }
    }
}