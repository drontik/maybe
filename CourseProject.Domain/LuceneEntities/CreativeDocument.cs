using System.Collections.Generic;
using System.Linq;
using CourseProject.Domain.Entities;
using CourseProject.Domain.LuceneEngine;

namespace CourseProject.Domain.LuceneEntities
{
    class CreativeDocument : ADocument
    {
        private string name;
        private string description;
        private string userName;

        private IEnumerable<string> tags;
        private IEnumerable<string> chapterNames;
        private IEnumerable<string> chapterBodies;
        private IEnumerable<string> comments;
        private IEnumerable<string> commentsUserName;


        [SearchField]
        public IEnumerable<string> CommentsUserName
        {
            get { return commentsUserName; }
            set
            {
                commentsUserName = value;

                foreach (var _name in commentsUserName)
                {
                    AddParameterToDocumentNoStoreParameter("CommentsUserName", _name);
                }
            }
        }

        [SearchField]
        public IEnumerable<string> Tags
        {
            get { return tags; }
            set
            {
                tags = value;

                foreach (var tag in tags)
                {
                    AddParameterToDocumentNoStoreParameter("Tags", tag);
                }
            }
        }

        [SearchField]
        public IEnumerable<string> ChapterNames
        {
            get { return chapterNames; }
            set
            {
                chapterNames = value;

                foreach (var chapterName in chapterNames)
                {
                    AddParameterToDocumentNoStoreParameter("ChapterNames", chapterName);
                }
            }
        }

        [SearchField]
        public IEnumerable<string> ChapterBodies
        {
            get { return chapterBodies; }
            set
            {
                chapterBodies = value;

                foreach (var chapterBody in chapterBodies)
                {
                    AddParameterToDocumentNoStoreParameter("ChapterBodies", chapterBody);
                }
            }
        }

        [SearchField]
        public IEnumerable<string> Comments
        {
            get { return comments; }
            set
            {
                comments = value;

                foreach (var comment in comments)
                {
                    AddParameterToDocumentNoStoreParameter("Comments", comment);
                }
            }
        }

        [SearchField]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                AddParameterToDocumentNoStoreParameter("Name", name);
            }
        }

        [SearchField]
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;

                AddParameterToDocumentNoStoreParameter("UserName", userName);
            }
        }

        [SearchField]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;

                AddParameterToDocumentNoStoreParameter("Description", description);
            }
        }


        public static explicit operator CreativeDocument(Creative creative)
        {
            foreach (var chapter in creative.Chapters)
            {
                if (chapter.Body == null)
                {
                    chapter.Body = "";
                }

            }

            return new CreativeDocument
            {
                Id = creative.Id,
                Name = creative.Name,
                Description = creative.Description,
                Tags = creative.Tags.Select(x => x.Name),
                ChapterBodies = creative.Chapters.Select(x => x.Body),
                ChapterNames = creative.Chapters.Select(x => x.Name),
                Comments = creative.Comments.Select(x => x.Text),
                CommentsUserName = creative.Comments.Select(x => x.User.UserName),
                UserName = creative.User.UserName

            };

        }
    }
}
