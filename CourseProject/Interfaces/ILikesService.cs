using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ILikesService
    {
        Task<IEnumerable<NewLikeModel>> GetLikes(int commentId);

        Task<NewCommentModel> AddLike(NewLikeModel model);
    }
}
