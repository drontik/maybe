using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork db;

        public RatingService(IUnitOfWork repo)
        {
            db = repo;
        }

        public async Task<IEnumerable<NewRatingModel>> AddRating(NewRatingModel model)
        {
            var user = await db.Users.FindUser(model.UserName);

            if (user == null) return null;

            if (user.Roles.All(x => x.RoleId != "1"))
            {
                if (db.Ratings.GetAll().ToList().Any(x => x.User == user && x.CreativeId == model.CreativeId))
                {
                    return null;
                }
            }

            db.Ratings.Add(await InitRating(model));

            db.Save();

            return InitRatingModel(db.Ratings.Find(x => x.CreativeId == model.CreativeId));
        }

        private async Task<Rating> InitRating(NewRatingModel model)
        {
            return new Rating
            {
                CreativeId = model.CreativeId,
                Value = model.Value,
                User = await db.Users.FindUser(model.UserName)
            };
        }

        public IEnumerable<NewRatingModel> InitRatingModel(IEnumerable<Rating> ratings)
        {
            return ratings.Select(rating => new NewRatingModel
            {
                Id = rating.Id,
                UserName = rating.User.UserName,
                CreativeId = rating.CreativeId,
                Value = rating.Value

            }).ToList();
        }
    }
}