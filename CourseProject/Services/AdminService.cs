using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork db;
        private readonly IAccountService service;
        private readonly IRatingService ratingService;
        private readonly ICommentsService commentService;
      

        public AdminService(IUnitOfWork repo, IAccountService serv, IRatingService ratingServ, ICommentsService commentServ)
        {
            db = repo;
            service = serv;
            ratingService = ratingServ;
            commentService = commentServ;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            return db.Users.GetAllUsers().Select(x => service.InitUserViewModel(x)).ToList();
        }

        public IEnumerable<Medal> GetMedals()
        {
            return db.Medals.GetAll().ToList();
        }

        public IEnumerable<NewRatingModel> GetRatings()
        {
            var result = db.Ratings.GetAll();

            return ratingService.InitRatingModel(result);
        }

        public IEnumerable<NewCommentModel> GetComments()
        {
            var result = db.Comments.GetAll();

            return commentService.InitCommentsModel(result);
        }

        public IEnumerable<Tag> GetTags()
        {
            return db.Tags.GetAll();
          
        }

        public IEnumerable<Chapter> GetChapters()
        {
           return db.Chapters.GetAll();
        }

        public async Task<IEnumerable<CreativeCategory>> DeleteCategory(int categoryId)
        {
            if (await db.Categories.Remove(categoryId))
            {
                db.Save();

                return db.Categories.GetAll();
            }

            return null;
        }

        public async Task<IEnumerable<UserViewModel>> SaveUserData(UserViewModel model)
        {
            var user = await db.Users.FindUserById(model.Id);

            var result = service.InitApplicationUser(model, user);

            await db.Users.UpdateUser(result);
           
            return db.Users.GetAllUsers().Select(x => service.InitUserViewModel(x)).ToList();
        }

        public async Task<IdentityResult> DeleteUser(UserViewModel model)
        {
            var user = await db.Users.FindUserById(model.Id);
            db.Ratings.RemoveRange(db.Ratings.Find(x => x.User.Id == user.Id));
            db.Comments.RemoveRange(db.Comments.Find(x => x.User.Id == user.Id));
            db.Likes.RemoveRange(db.Likes.Find(x => x.User.Id == user.Id));
            db.Creatives.RemoveRange(db.Creatives.Find(x => x.User.Id == user.Id));
            db.ChapterStore.RemoveRange(db.ChapterStore.Find(x=>x.ApplicationUserId == user.Id));

            db.Save();

            return await db.Users.DeleteUser(user);
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordModel model)
        {
            return await db.Users.ResetPassword(model.UserId, model.NewPassword);
        }

        public async Task<IEnumerable<Tag>> SaveTag(TagsViewModel model)
        {
            if (model.Id == 0)
            {
                db.Tags.Add(new Tag { Name = model.Name });
            }
            else
            {
                var editTag = await db.Tags.Get(model.Id);

                editTag.Name = model.Name;
            }

            db.Save();

            return db.Tags.GetAll();
        }

        public async Task<IEnumerable<NewRatingModel>> SaveRating(NewRatingModel model)
        {
            var editRating = await db.Ratings.Get(model.Id);

            editRating.Value = model.Value;

            db.Save();

            return ratingService.InitRatingModel(db.Ratings.GetAll());
        }

        public async Task<IEnumerable<NewCommentModel>> SaveComment(NewCommentModel model)
        {
            var editRating = await db.Comments.Get(model.Id);

            editRating.Text = model.Text;

            db.Save();

            return commentService.InitCommentsModel(db.Comments.GetAll());
        }

        public async Task<IEnumerable<NewCommentModel>> DeleteComment(int id)
        {
            if (await db.Comments.Remove(id))
            {
                db.Save();

                return commentService.InitCommentsModel(db.Comments.GetAll());
            }

            return null;
        }

        public async Task<IEnumerable<Chapter>> DeleteChapter(int chapterId)
        {
           var result = await db.Chapters.Remove(chapterId);

            if (result)
            {
                db.Save();

                return db.Chapters.GetAll();
            }
            return null;
        }

        public async Task<IEnumerable<CreativeCategory>> SaveCategory(NewCategoryModel model)
        {
            if (model.Id == 0)
            {
                db.Categories.Add(new CreativeCategory { Name = model.Name });
            }
            else
            {
                var editCategory = await db.Categories.Get(model.Id);

                editCategory.Name = model.Name;
            }

            db.Save();

            return db.Categories.GetAll();
        }

        public async Task<IEnumerable<Tag>> DeleteTag(int id)
        {
            if (await db.Tags.Remove(id))
            {
                db.Save();

                return db.Tags.GetAll();
            }

            return null;
        }

        public async Task<IEnumerable<Rating>> DeleteRating(int id)
        {
            if (await db.Ratings.Remove(id))
            {
                db.Save();

                return db.Ratings.GetAll();
            }

            return null;
        }
    }
}