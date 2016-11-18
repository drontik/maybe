using System;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;

namespace CourseProject.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext db;

        private CreativesRepository creativesRepository;
        private CommentsRepository commentsRepository;
        private LikesRepository likesRepository;
        private ChaptersRepository chaptersRepository;
        private CreativeCategoryRepository categoryRepository;
        private RatingsRepository ratingsRepository;
        private MedalsRepository medalsRepository;
        private TagsRepository tagsRepository;
        private UsersRepository usersRepository;
        private ChapterStoreRepository chapterStoreRepository;

        private bool isDisposed;

        public EfUnitOfWork(DatabaseContext context)
        {
            db = context;
        }

        public IUsersRepository Users
        {
            get { return usersRepository ?? (usersRepository = new UsersRepository(db)); }
        }

        public IRepository<ChapterStore> ChapterStore
        {
            get { return chapterStoreRepository ?? (chapterStoreRepository = new ChapterStoreRepository(db)); }
        }

        public IRepository<Creative> Creatives
        {
            get { return creativesRepository ?? (creativesRepository = new CreativesRepository(db)); }
        }

        public IRepository<CreativeCategory> Categories
        {
            get { return categoryRepository ?? (categoryRepository = new CreativeCategoryRepository(db)); }
        }

        public IRepository<Rating> Ratings
        {
            get { return ratingsRepository ?? (ratingsRepository = new RatingsRepository(db)); }
        }

        public IRepository<Medal> Medals
        {
            get { return medalsRepository ?? (medalsRepository = new MedalsRepository(db)); }
        }

        public IRepository<Comment> Comments
        {
            get { return commentsRepository ?? (commentsRepository = new CommentsRepository(db)); }
        }

        public IRepository<Chapter> Chapters
        {
            get { return chaptersRepository ?? (chaptersRepository = new ChaptersRepository(db)); }
        }

        public IRepository<Like> Likes
        {
            get { return likesRepository ?? (likesRepository = new LikesRepository(db)); }
        }

        public IRepository<Tag> Tags
        {
            get { return tagsRepository ?? (tagsRepository = new TagsRepository(db)); }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                db.Dispose();
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}