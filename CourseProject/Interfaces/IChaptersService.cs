using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface IChaptersService
    {
        Task<NewChapterModel> GetChapter(int chapterId);

        Task<IEnumerable<NewChapterModel>> DeleteChapter(int chapterId);
        
        bool AddOrUpdateChapter(NewChapterModel model);

        NewChapterModel InitChapterViewModel(Chapter chapter);

        Task SetRememberedChapter(RememberChapterModel model);

        Task<ChapterStore> GetRememberedChapter(RememberChapterModel model);
    }
}
