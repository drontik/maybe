using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<UserViewModel> GetUsers();
        IEnumerable<Medal> GetMedals();
        IEnumerable<NewRatingModel> GetRatings();
        IEnumerable<NewCommentModel> GetComments();
        IEnumerable<Tag> GetTags();
        IEnumerable<Chapter> GetChapters();

        Task<IdentityResult> DeleteUser(UserViewModel model);
        Task<IEnumerable<Tag>> DeleteTag(int id);
        Task<IEnumerable<Rating>> DeleteRating(int id);
        Task<IEnumerable<NewCommentModel>> DeleteComment(int id);
        Task<IEnumerable<Chapter>> DeleteChapter(int chapterId);
        Task<IEnumerable<CreativeCategory>> DeleteCategory(int categoryId);

        Task<IEnumerable<UserViewModel>> SaveUserData(UserViewModel model);
        Task<IEnumerable<Tag>> SaveTag(TagsViewModel model);
        Task<IEnumerable<NewRatingModel>> SaveRating(NewRatingModel model);
        Task<IEnumerable<NewCommentModel>> SaveComment(NewCommentModel model);
        Task<IEnumerable<CreativeCategory>> SaveCategory(NewCategoryModel model);

        Task<IdentityResult> ResetPassword(ResetPasswordModel model);

    }
}