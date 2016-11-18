using Lucene.Net.Documents;

namespace CourseProject.Domain.LuceneEngine
{
    public abstract class ADocument : IDocument
    {
        private int id;
        
        [SearchField]
        public int Id
        {
            set
            {
                id = value;
                AddParameterToDocument("Id", id, Field.Store.YES, Field.Index.NOT_ANALYZED);
            }
            get { return id; }
        }

        public string TypeString { get; set; }
        
        private readonly Document document;

        public Document Document
        {
            get { return document; }
        }

        protected ADocument()
        {
            document = new Document();
        }
 
        protected ADocument(string typeString)
        {
            TypeString = typeString;
        }
  
        private void AddParameterToDocument(string name, dynamic value, Field.Store store, Field.Index index)
        {
            document.Add(new Field(name, value.ToString(), store, index));
        }

        protected void AddParameterToDocumentStoreParameter(string name, dynamic value)
        {
            AddParameterToDocument(name, value,Field.Store.YES,Field.Index.ANALYZED);
        }

        protected void AddParameterToDocumentNoStoreParameter(string name, dynamic value)
        {
            AddParameterToDocument(name, value,Field.Store.NO,Field.Index.ANALYZED);
        }
       
    }
    
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class SearchField : System.Attribute
    {
        public string[] CombinedSearchFields;


        public SearchField(params string[] values)
        {
            CombinedSearchFields = values;
        }
    }
}
