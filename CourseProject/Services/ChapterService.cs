using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Services
{
    public class ChapterService : IChaptersService
    {
        private readonly IUnitOfWork db;

        public ChapterService(IUnitOfWork repo)
        {
            db = repo;
        }

        public async Task<IEnumerable<NewChapterModel>> DeleteChapter(int chapterId)
        {
            var chapter = await db.Chapters.Get(chapterId);

            var result = await db.Chapters.Remove(chapterId);

            if (result)
            {
                db.Save();

                return InitChaptersViewModel(db.Chapters.Find(x => x.CreativeId == chapter.CreativeId));
            }
            return null;
        }

        public bool AddOrUpdateChapter(NewChapterModel model)
        {
            var chapter = InitChapter(model);

            AddOrUpdateChapter(model, chapter);

            db.Save();

            return true;
        }

        public async Task SetRememberedChapter(RememberChapterModel model)
        {
            var user = await db.Users.FindUser(model.UserName);

            var userStoredChapters = db.ChapterStore.Find(x => x.ApplicationUserId == user.Id).ToList();

            var isAlreadyExist = userStoredChapters.Any(x => x.CreativeId == model.CreativeId);

            if (!isAlreadyExist)
            {
                db.ChapterStore.Add(new ChapterStore
                {
                    ChapterId = model.ChapterId,
                    CreativeId = model.CreativeId,
                    ApplicationUserId = user.Id
                });
            }
            else
            {
                var chapter = userStoredChapters.First(x => x.CreativeId == model.CreativeId);

                chapter.ChapterId = model.ChapterId;
            }

            db.Save();
        }

        public async Task<ChapterStore> GetRememberedChapter(RememberChapterModel model)
        {
            var user = await db.Users.FindUser(model.UserName);

            var userStoredChapters = db.ChapterStore.Find(x => x.ApplicationUserId == user.Id).ToList();

            var isAlreadyExist = userStoredChapters.Any(x => x.CreativeId == model.CreativeId);

            return isAlreadyExist ? userStoredChapters.First(x => x.CreativeId == model.CreativeId) : null;
        }

        public async Task<NewChapterModel> GetChapter(int id)
        {
            return InitChapterViewModel(await db.Chapters.Get(id));
        }

        private void AddOrUpdateChapter(NewChapterModel model, Chapter chapter)
        {
            if (model.Id == 0)
            {
                db.Chapters.Add(chapter);
            }
            else
            {
                db.Chapters.Update(chapter);
            }
            db.Save();
        }


        private static Chapter InitChapter(NewChapterModel model)
        {
            return new Chapter
            {
                Id = model.Id,
                Name = model.Name,
                Body = model.Text,
                Number = model.Number,
                CreativeId = model.CreativeId,
                Created = DateTime.Now
            };
        }

        public NewChapterModel InitChapterViewModel(Chapter chapter)
        {
            if (chapter != null)
            {
                return new NewChapterModel
                {
                    Id = chapter.Id,
                    CreativeId = chapter.CreativeId,
                    Name = chapter.Name,
                    Number = chapter.Number,
                    Text = chapter.Body,
                    CreatedOn = chapter.Created.ToString(CultureInfo.CurrentCulture),
                    Edit = false
                };
            }
            return new NewChapterModel();
        }

        private static IEnumerable<NewChapterModel> InitChaptersViewModel(IEnumerable<Chapter> list)
        {
            return list.Select(c => new NewChapterModel
            {
                Id = c.Id,
                CreativeId = c.CreativeId,
                Name = c.Name,
                Number = c.Number,
                Text = c.Body,
                CreatedOn = c.Created.ToString(CultureInfo.CurrentCulture),
                Edit = false
            });
        }
    }
}
