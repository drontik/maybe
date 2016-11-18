using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Services
{
    public class TagsService : ITagsService
    {
        private readonly IUnitOfWork db;

        public TagsService(IUnitOfWork repo)
        {
            db = repo;
        }

        public IEnumerable<TagsViewModel> GetAllTags()
        {
            return InitTagsViewModel(db.Tags.GetAll().ToList(),db.Tags.GetAll().ToList()); 
        }

        public async Task<IEnumerable<TagsViewModel>> GetCreativeTags(int creativeId)
        {
            var creative = await db.Creatives.Get(creativeId);

            return creative != null ? InitTagsViewModel(creative.Tags,db.Tags.GetAll().ToList()) : new List<TagsViewModel>();
        }

        public IEnumerable<TagsViewModel> SaveTags(int creativeId, IEnumerable<Tag> tags)
        {
            db.Tags.RemoveRange(db.Tags.GetAll());

            db.Tags.AddRange(tags);

            db.Save();

            return InitTagsViewModel(db.Tags.Find(x => x.CreativeId == creativeId), db.Tags.GetAll().ToList());
        }

        public IEnumerable<TagsViewModel> InitTagsViewModel(IEnumerable<Tag> inputTags, List<Tag> allTags)
        {
            var result = new List<TagsViewModel>();

            var tagGroups = inputTags.GroupBy(x => x.Name);

            foreach (var tagGroup in tagGroups)
            {
                var firstTag = tagGroup.First();

                if (allTags.Where(x => x.CreativeId != null).Count(x => x.Name == firstTag.Name) == 0)
                {
                    continue;
                }

                result.Add(new TagsViewModel
                {
                    Id = firstTag.Id,
                    Name = firstTag.Name,
                    Count = allTags.Where(x=>x.CreativeId != null).Count(x => x.Name == firstTag.Name),
                    CreativeId = firstTag.CreativeId
                });
            }

            return result;
        }

        public TagsViewModel InitTagViewModel(Tag tag)
        {
            return new TagsViewModel
            {
                CreativeId = tag.CreativeId,
                Id = tag.Id,
                Name = tag.Name
            };
        }
    }
}