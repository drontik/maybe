using System;
using CourseProject.Domain.Entities;

namespace CourseProject.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Creative> Creatives { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Chapter> Chapters { get; }
        IRepository<Like> Likes { get; }
        IRepository<CreativeCategory> Categories { get; }
        IRepository<Rating> Ratings { get; }
        IRepository<Medal> Medals { get; }
        IRepository<Tag> Tags { get; }
        IRepository<ChapterStore> ChapterStore { get;}
        IUsersRepository Users { get; }
       
        void Save();
    }
}
