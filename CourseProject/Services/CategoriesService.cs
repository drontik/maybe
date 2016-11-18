using System.Collections.Generic;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;

namespace CourseProject.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork db;

        public CategoriesService(IUnitOfWork repo)
        {
            db = repo;
        }

        public IEnumerable<CreativeCategory> GetCategories()
        {
            return db.Categories.GetAll();
        }
    }
}