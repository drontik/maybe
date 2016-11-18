using System.IO;
using Lucene.Net.Store;

namespace CourseProject.Domain.LuceneEngine
{
    public abstract class BaseSearch
    {
        private const string LuceneIndexFolder = "LuceneIndex";
        private readonly string dataFolder;

        public string DataFolder
        {
            get { return DataFolder; }
        }

        public FSDirectory LuceneDirectory { get; }

        protected BaseSearch(string dataFolder)
        {
            this.dataFolder = dataFolder;

            var di = new DirectoryInfo(Path.Combine(dataFolder,LuceneIndexFolder));

            if (!di.Exists)
            {
                di.Create();
            }

            LuceneDirectory = FSDirectory.Open(di.FullName);
        }
    }
}
