using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ICommentsService
    {
        IEnumerable<NewCommentModel> GetComments(int creativeId);

        Task<IEnumerable<NewCommentModel>> AddComment(NewCommentModel model);

        Task<IEnumerable<NewCommentModel>> DeleteComment(int id);

        IEnumerable<NewCommentModel> InitCommentsModel(IEnumerable<Comment> commentsList);
    }
}