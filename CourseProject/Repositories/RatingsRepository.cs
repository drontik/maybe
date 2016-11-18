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
    public class RatingsRepository : IRepository<Rating>
    {
        private readonly DatabaseContext db;

        public RatingsRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<Rating> GetAll()
        {
           return db.Ratings.ToList();
        }

        public async Task<Rating> Get(int id)
        {
            return await db.Ratings.FindAsync(id);
        }

        public IEnumerable<Rating> Find(Func<Rating, bool> predicate)
        {
            return db.Ratings.Where(predicate).ToList();
        }

        public void Add(Rating item)
        {
            db.Ratings.Add(item);
        }

        public void Update(Rating item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var rating = await db.Ratings.FindAsync(id);

            if (rating != null)
            {
                db.Ratings.Remove(rating);

                return true;
            }

            return false;
        }

        public void AddRange(IEnumerable<Rating> range)
        {
            db.Ratings.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Rating> range)
        {
            db.Ratings.RemoveRange(range);
        }
    }
}