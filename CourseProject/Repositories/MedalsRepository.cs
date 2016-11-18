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
    public class MedalsRepository : IRepository<Medal>
    {
        private readonly DatabaseContext db;

        public MedalsRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<Medal> GetAll()
        {
            return db.Medals.ToList();
        }

        public async Task<Medal> Get(int id)
        {
            return await db.Medals.FindAsync(id);
        }

        public IEnumerable<Medal> Find(Func<Medal, bool> predicate)
        {
            return db.Medals.Where(predicate).ToList();
        }

        public void Add(Medal item)
        {
            db.Medals.Add(item);
        }

        public void Update(Medal item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var medal = await db.Medals.FindAsync(id);

            if (medal != null)
            {
                db.Medals.Remove(medal);

                return true;
            }

            return false;
        }

        public void AddRange(IEnumerable<Medal> range)
        {
            db.Medals.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Medal> range)
        {
            db.Medals.RemoveRange(range);
        }
    }
}