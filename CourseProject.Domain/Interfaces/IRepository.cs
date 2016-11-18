using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseProject.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> predicate);

        Task<T> Get(int id);

        Task<bool> Remove(int id);

        void Add(T item);

        void Update(T item);

        void AddRange(IEnumerable<T> range);

        void RemoveRange(IEnumerable<T> range);
    }
}
