using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.LuceneEngine;
using CourseProject.Domain.LuceneEntities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Services.CompareHelpers;

namespace CourseProject.Services
{
    public class CreativeService : ICreativeService
    {
        private readonly string dataFolder = HttpContext.Current.Server.MapPath("~/Uploads");

        private readonly IUnitOfWork db;
        private readonly IChaptersService chapterService;
        private readonly ICommentsService commentsService;
        private readonly ITagsService tagsService;

        public CreativeService(IUnitOfWork repo, IChaptersService chapterServ,
            ITagsService tagServ, ICommentsService commentsServ)
        {
            db = repo;
            chapterService = chapterServ;
            tagsService = tagServ;
            commentsService = commentsServ;
        }

        public async Task<NewCreativeModel> UpdateCreative(NewCreativeModel model)
        {
            var creative = await db.Creatives.Get(model.Id);

            creative.Name = model.Name;

            creative.Description = model.Description;

            foreach (var chapter in model.Chapters)
            {
                chapterService.AddOrUpdateChapter(chapterService.InitChapterViewModel(chapter));
            }

            var tagsToDelete = new List<Tag>();

            foreach (var tag in creative.Tags)
            {
                if (!model.Tags.Contains(tagsService.InitTagViewModel(tag), new CompareTagViewModels()))
                {
                    tagsToDelete.Add(tag);
                }
            }

            db.Tags.RemoveRange(tagsToDelete);

            foreach (var newTag in model.Tags.Where(newTag => newTag.CreativeId != creative.Id))
            {
                newTag.CreativeId = creative.Id;

                db.Tags.Add(new Tag {CreativeId = newTag.CreativeId, Name = newTag.Name});
            }
         
            db.Creatives.Update(creative);

            db.Save();

            return InitCreativeModel(creative);
        }

        public async Task<IEnumerable<NewCreativeModel>> CreateCreative(NewCreativeModel model)
        {
            var creative = await InitNewCreative(model);

            db.Creatives.Add(creative);

            db.Save();

            var res = db.Creatives.Find(c => string.Equals(c.User.UserName, model.UserName, StringComparison.CurrentCultureIgnoreCase));

            return InitCreativesModel(res);
        }
     

        public async Task<IEnumerable<NewCreativeModel>> DeleteCreative(int id)
        {
            var currentCreative = await db.Creatives.Get(id);

            if (currentCreative == null)
            {
                return null;
            }

            var userId = currentCreative.User.Id;

            db.Tags.RemoveRange(currentCreative.Tags);

            var result = await db.Creatives.Remove(id);

            if (!result) return null;

            db.Save();

            return InitCreativesModel(db.Creatives.Find(x=>x.User.Id == userId)) ;
        }

        public async Task<IEnumerable<NewCreativeModel>> SearchCreatives(SearchViewModel model)
        {
            var results = new List<Creative>();

            var writer = new CreativeWriter(dataFolder);

            var searcher = new CreativeSearcher(dataFolder);

            writer.AddUpdateCreativesToIndex(db.Creatives.GetAll().ToList());

            var searchResults = GetSearchResults(model, searcher);

            foreach (var searchResult in searchResults)
            {
                foreach (var item in searchResult.SearchResultItems)
                {
                    var creative = await db.Creatives.Get(item.Id);

                    if (creative != null && !results.Contains(creative))
                    {
                        results.Add(creative);
                    }
                }
            }
            return InitCreativesModel(results);
        }

        public IEnumerable<NewCreativeModel> SearchCreativesByCategory(int categoryId)
        {
            var results = db.Creatives.Find(x => x.Category.Id == categoryId);

            return InitCreativesModel(results);
        }

        public async Task<NewCreativeModel> GetCreativeById(int id)
        {
            var result = await db.Creatives.Get(id);

            return result != null ? InitCreativeModel(result) : null;
        }

        public async Task<IEnumerable<NewCreativeModel>> GetUsersCreatives(string userName)
        {
            var user = await db.Users.FindUser(userName);

            if (user == null) return null;

            var list = db.Creatives.Find(x => x.User == user).ToList();

            return InitCreativesModel(list);
        }

        public IEnumerable<NewCreativeModel> GetAllCreatives()
        {
            return InitCreativesModel(db.Creatives.GetAll().OrderBy(x=>x.Created).ToList());
        }

        public IEnumerable<NewCreativeModel> GetPartialCreatives(int delimiter)
        {
            var all = db.Creatives.GetAll().Reverse().ToList();

            var countPerPart = all.Count/5;

            if (countPerPart == 0)
            {
                return delimiter == 0 ? InitCreativesModel(all) : new List<NewCreativeModel>();
            }

            var res = InitCreativesModel(all.Skip(countPerPart*delimiter).Take(countPerPart));

            return res;
        }

        public IEnumerable<NewCreativeModel> GetMostPopularCreatives()
        {
            var all = db.Creatives.GetAll().OrderBy(x => x.Comments.Count);

            return InitCreativesModelLightWeight(all.Skip(all.Count() - 5)).Reverse();
        }

        public IEnumerable<NewCreativeModel> GetMostRatedCreatives()
        {
            var all = InitCreativesModelLightWeight(db.Creatives.GetAll()).ToList();

            var res = all.OrderBy(x => x.AvgRating).Skip(all.Count - 5).Reverse();

            return res;
        }

        private async Task<Creative> InitNewCreative(NewCreativeModel model)
        {

            var creative = new Creative
            {
                Name = model.Name,
                Description = model.Description,
                Category = await db.Categories.Get(model.CategoryId),
                Tags = model.Tags.Select(x => new Tag {Name = x.Name}).ToList(),
                Created = DateTime.Now,
                Rating = new List<Rating>(),
                Comments = new List<Comment>()
            };

            var user = await db.Users.FindUser(model.UserName);

            creative.User = user;

            return creative;
        }

        public NewCreativeModel InitCreativeModel(Creative creative)
        {
          return new NewCreativeModel
            {
                Id = creative.Id,
                Chapters = creative.Chapters,
                Comments = commentsService.InitCommentsModel(creative.Comments),
                UserName = creative.User.UserName,
                Name = creative.Name,
                Description = creative.Description,
                Category = creative.Category,
                Rating = creative.Rating,
                Tags = tagsService.InitTagsViewModel(creative.Tags, db.Tags.GetAll().ToList()),
                Created = creative.Created.ToShortDateString() + " " + creative.Created.ToShortTimeString(),
                AvgRating = creative.Rating.Any() ? creative.Rating.Average(x => x.Value) : 0,
                AvatarUri = creative.User.AvatarUri
          };
        }

        private IEnumerable<NewCreativeModel> InitCreativesModel(IEnumerable<Creative> list)
        {
            return list.Select(creative => new NewCreativeModel
            {
                Id = creative.Id,
                Chapters = creative.Chapters,
                Comments = commentsService.InitCommentsModel(creative.Comments),
                UserName = creative.User.UserName,
                Name = creative.Name,
                Description = creative.Description,
                Category = creative.Category,
                Rating = creative.Rating,
                Tags = tagsService.InitTagsViewModel(creative.Tags, db.Tags.GetAll().ToList()),
                Created = creative.Created.ToShortDateString() + " " + creative.Created.ToShortTimeString(),
                AvgRating = creative.Rating.Any() ? creative.Rating.Average(x=>x.Value) : 0,
                AvatarUri = creative.User.AvatarUri

            }).ToList();
        }

        private IEnumerable<NewCreativeModel> InitCreativesModelLightWeight(IEnumerable<Creative> list)
        {
            return list.Select(creative => new NewCreativeModel
            {
                Id = creative.Id,
                Name = creative.Name,
                Comments = commentsService.InitCommentsModel(creative.Comments),
                AvgRating = creative.Rating.Any() ? creative.Rating.Average(x => x.Value) : 0
            }).ToList();
        }

        private IEnumerable<SearchResult> GetSearchResults(SearchViewModel model, CreativeSearcher searcher)
        {
            var searchResults = new List<SearchResult>();

            if (model.ChapterText && model.CommentAuthor && model.CommentText && model.CreativeAuthor
               && model.CreativeDescription && model.CreativeName && model.TagName && model.ChapterName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, string.Empty));
                return searchResults;
            }

            if (model.ChapterName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "ChapterNames"));
            }

            if (model.ChapterText)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "ChapterBodies"));
            }

            if (model.CommentAuthor)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "CommentsUserName"));
            }

            if (model.CommentText)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Comments"));
            }

            if (model.CreativeAuthor)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "UserName"));
            }

            if (model.CreativeDescription)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Description"));
            }

            if (model.CreativeName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Name"));
            }

            if (model.TagName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Tags"));
            }

            return searchResults;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }

}