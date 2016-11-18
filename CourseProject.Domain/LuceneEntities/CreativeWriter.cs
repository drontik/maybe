using System.Collections.Generic;
using System.Linq;
using CourseProject.Domain.Entities;
using CourseProject.Domain.LuceneEngine;

namespace CourseProject.Domain.LuceneEntities
{
    public class CreativeWriter : BaseWriter
    {
        public CreativeWriter(string dataFolder)
            : base(dataFolder)
        {
        }

        public void AddUpdateCreativeToIndex(Creative person)
        {
            AddUpdateItemsToIndex(new List<CreativeDocument> { (CreativeDocument)person });
        }

        public void AddUpdateCreativesToIndex(List<Creative> people)
        {
            AddUpdateItemsToIndex(people.Select(p => (CreativeDocument) p).ToList());
        }

        public void DeleteCreativeFromIndex(Creative person)
        {
            DeleteItemsFromIndex(new List<CreativeDocument> { (CreativeDocument)person });
        }

        public void DeleteCreativeFromIndex(int id)
        {
            DeleteItemsFromIndex(new List<CreativeDocument> { new CreativeDocument { Id = id } });
        }
    }
}
