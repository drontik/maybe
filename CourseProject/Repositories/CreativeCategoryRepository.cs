using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;

namespace CourseProject.Repositories
{
    public class CreativeCategoryRepository : IRepository<CreativeCategory>
    {
        private readonly DatabaseContext db;

        public CreativeCategoryRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<CreativeCategory> GetAll()
        {
           return db.Categories.ToList();
        }

        public async Task<CreativeCategory> Get(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public IEnumerable<CreativeCategory> Find(Func<CreativeCategory, bool> predicate)
        {
            return db.Categories.Where(predicate).ToList();
        }

        public void Add(CreativeCategory item)
        {
            db.Categories.Add(item);
        }

        public void Update(CreativeCategory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var category = await db.Categories.FindAsync(id);

            if (category != null)
            {
                db.Categories.Remove(category);

                return true;
            }

            return false;
        }

        public void AddRange(IEnumerable<CreativeCategory> range)
        {
            db.Categories.AddRange(range);
        }

        public void RemoveRange(IEnumerable<CreativeCategory> range)
        {
            db.Categories.RemoveRange(range);
        }
    }
}