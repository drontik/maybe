using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ITagsService
    {
        TagsViewModel InitTagViewModel(Tag tag);

        Task<IEnumerable<TagsViewModel>> GetCreativeTags(int creativeId);

        IEnumerable<TagsViewModel> GetAllTags();
        IEnumerable<TagsViewModel> SaveTags(int creativeId, IEnumerable<Tag> tags);
        IEnumerable<TagsViewModel> InitTagsViewModel(IEnumerable<Tag> inputTags, List<Tag> allTags);
    }
}