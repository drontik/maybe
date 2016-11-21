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
    class CreativesRepository : IRepository<Creative>
    {
        private readonly DatabaseContext db;

        public CreativesRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<Creative> GetAll()
        {
            return db.Creatives.ToList();
        }

        public async Task<Creative> Get(int id)
        {
            return await db.Creatives.FindAsync(id);
        }

        public IEnumerable<Creative> Find(Func<Creative, bool> predicate)
        {
            return db.Creatives.Where(predicate).ToList();
        }

        public void Add(Creative item)
        {
            db.Creatives.Add(item);
        }

        public void Update(Creative item)
        {
           db.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var creative = await db.Creatives.Include(x => x.Tags).SingleAsync(x => x.Id == id);

            if (creative != null)
            {
                db.Creatives.Remove(creative);

                return true;
            }

            return false;
        }

        public void AddRange(IEnumerable<Creative> range)
        {
            db.Creatives.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Creative> range)
        {
            foreach (var id in range)
            {
                    db.Tags.RemoveRange(id.Tags);
            }
            db.Creatives.RemoveRange(range);
        }
    }
}
