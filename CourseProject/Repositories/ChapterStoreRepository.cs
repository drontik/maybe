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
    class ChapterStoreRepository : IRepository<ChapterStore>
    {
        private readonly DatabaseContext db;

        public ChapterStoreRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<ChapterStore> GetAll()
        {
            return db.ChapterStore.ToList();
        }

        public IEnumerable<ChapterStore> Find(Func<ChapterStore, bool> predicate)
        {
            return db.ChapterStore.Where(predicate).ToList();
        }

        public async Task<ChapterStore> Get(int id)
        {
            return await db.ChapterStore.FindAsync(id);
        }

        public async Task<bool> Remove(int id)
        {
            var chapter = await db.Chapters.FindAsync(id);

            if (chapter == null) return false;

            db.Chapters.Remove(chapter);

            return true;
        }

        public void Add(ChapterStore item)
        {
            db.ChapterStore.Add(item);
        }

        public void Update(ChapterStore item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void AddRange(IEnumerable<ChapterStore> range)
        {
            db.ChapterStore.AddRange(range);
        }

        public void RemoveRange(IEnumerable<ChapterStore> range)
        {
            db.ChapterStore.RemoveRange(range);
        }
    }
}
