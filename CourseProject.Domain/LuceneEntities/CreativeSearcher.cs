using CourseProject.Domain.LuceneEngine;

namespace CourseProject.Domain.LuceneEntities
{
    public class CreativeSearcher: BaseSearcher
    {
        public CreativeSearcher(string dataFolder) : base(dataFolder)
        {
        }

        public SearchResult SearchCreative(string searchTerm, string field)
        {
            return Search<CreativeDocument>(field, searchTerm);
        }

    }
}
