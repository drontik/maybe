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
    class CommentsRepository : IRepository<Comment>
    {
        private readonly DatabaseContext db;

        public CommentsRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments.ToList();
        }

        public async Task<Comment> Get(int id)
        {
            return await db.Comments.FindAsync(id);
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return db.Comments.Where(predicate).ToList();
        }

        public void Add(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var comment = await db.Comments.FindAsync(id);

            if (comment != null)
            {
                db.Comments.Remove(comment);
                return true;
            }

            return false;
        }
      
        public void AddRange(IEnumerable<Comment> range)
        {
            db.Comments.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Comment> range)
        {
            db.Comments.RemoveRange(range);
        }
    }
}
