using System.Collections.Generic;

namespace CourseProject.Domain.LuceneEngine
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public List<SearchResultItem> SearchResultItems { get; set; }
        public int Hits { get; set; }
    }

    public class SearchResultItem
    {
        public int Id { get; set; }
        public float Score { get; set; }
    }
}
