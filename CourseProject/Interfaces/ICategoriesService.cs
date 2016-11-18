using System.Collections.Generic;
using CourseProject.Domain.Entities;

namespace CourseProject.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<CreativeCategory> GetCategories();
    }
}