using System.Collections.Generic;
using System.Linq;
using log4net;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace CourseProject.Domain.LuceneEngine
{
   public abstract class BaseWriter : BaseSearch
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BaseWriter));

        protected BaseWriter(string dataFolder):base(dataFolder)
        {
            Log.DebugFormat("Initialisation Writer with folder {0}", dataFolder);
        }

        private void AddItemToIndex(ADocument doc, IndexWriter writer)
        {
            Log.DebugFormat("Adding document to index: Type {0}");
            var query = new TermQuery(new Term("Id", doc.Id.ToString()));
            writer.DeleteDocuments(query);
            writer.AddDocument(doc.Document);
        }

        protected void AddUpdateItemsToIndex(IEnumerable<ADocument> docs)
        {
            Log.DebugFormat("Adding {0} items to index",docs.Count());
            var standardAnalyzer = new StandardAnalyzer(Version.LUCENE_30);

            using (var writer = new IndexWriter(LuceneDirectory, standardAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var doc in docs)
                {
                    Log.DebugFormat("Adding item to index: {0}: ",doc);
                    AddItemToIndex(doc, writer);
                }
                standardAnalyzer.Close();
                writer.Dispose();
            }
        }

        private void DeleteItemFromIndex(ADocument doc, IndexWriter writer)
        {
            Log.DebugFormat("Deleting item {0} from index",doc);
            var query = new TermQuery(new Term("Id", doc.Id.ToString()));
            writer.DeleteDocuments(query);
        }

        protected void DeleteItemsFromIndex(IEnumerable<ADocument> docs)
        {
            Log.DebugFormat("Deleting {0} items from index",docs.Count());
            var standardAnalyzer = new StandardAnalyzer(Version.LUCENE_30);

            using (var writer = new IndexWriter(LuceneDirectory, standardAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var doc in docs)
                {
                    Log.DebugFormat("Deleting item from index: {0}",doc);
                    DeleteItemFromIndex(doc, writer);
                }
                standardAnalyzer.Close();
                writer.Dispose();
            }
        }

        protected void Optimize()
        {
            Log.Debug("optimizing Lucene search index");
            var standardAnalyzer = new StandardAnalyzer(Version.LUCENE_30);

            using (var writer = new IndexWriter(LuceneDirectory, standardAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                standardAnalyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }
    }
}
