using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Repositories;

namespace CourseProject.Services
{
    public class MedalService : IMedalService
    {
        private readonly IUnitOfWork db;

        public MedalService(IUnitOfWork repo)
        {
            db = repo;
        }

        public async Task<ICollection<Medal>> CheckMedals(ApplicationUser user)
        {
            var userMedals = user.Medals ?? new List<Medal>();

            await CheckUserMedals(user, userMedals);

            return userMedals;

        }

        private async Task CheckUserMedals(ApplicationUser user, ICollection<Medal> userMedals)
        {
            await CheckUserDataMedal(user, userMedals);

            await CheckActiveLikerMedal(user, userMedals);

            await CheckCommentsMedal(user, userMedals);

            await CheckCreativesMedal(user, userMedals);

            await CheckSuperStarMedal(user, userMedals);

            await CheckSuperCommentatorMedal(user, userMedals);
        }

        private async Task CheckActiveLikerMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var likesCount = db.Likes.Find(x => x.User.Id == user.Id).Count();

            var likeMedal = await db.Medals.Get(3);

            if (likesCount >= 10 && !userMedals.Contains(likeMedal))
            {
                userMedals.Add(likeMedal);
            }
            else if (likesCount < 10 && userMedals.Contains(likeMedal))
            {
                userMedals.Remove(likeMedal);
            }
        }

        private async Task CheckUserDataMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var condition = user.Email != null && user.FirstName != null
                && user.LastName != null
                && user.PhoneNumber != null;

            var userDataMedal = await db.Medals.Get(6);

            if (condition && !userMedals.Contains(userDataMedal))
            {
                userMedals.Add(userDataMedal);
            }
            else if (!condition && userMedals.Contains(userDataMedal))
            {
                userMedals.Remove(userDataMedal);
            }
        }

        private async Task CheckCommentsMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var commentsMedal = await db.Medals.Get(1);

            var commentsCount = db.Comments.Find(x => x.User.Id == user.Id).Count();

            if (commentsCount >= 10 && !userMedals.Contains(commentsMedal))
            {
                userMedals.Add(commentsMedal);
            }
            else if (commentsCount < 10 && userMedals.Contains(commentsMedal))
            {
                userMedals.Remove(commentsMedal);
            }
        }

        private async Task CheckCreativesMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var creativesMedal = await db.Medals.Get(2);

            var creativeCount = db.Creatives.Find(x => x.User.Id == user.Id).Count();

            if (creativeCount >= 10 && !userMedals.Contains(creativesMedal))
            {
                userMedals.Add(creativesMedal);
            }
            else if (creativeCount < 10 && userMedals.Contains(creativesMedal))
            {
                userMedals.Remove(creativesMedal);
            }
        }

        private async Task CheckSuperStarMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var superStarMedal = await db.Medals.Get(4);

            var creatives = db.Creatives.Find(x => x.User.Id == user.Id).ToList();

            if (creatives.Any(x=>x.Comments.Count >= 10) && !userMedals.Contains(superStarMedal))
            {
                userMedals.Add(superStarMedal);
            }
            else if (creatives.Any(x => x.Comments.Count < 10) && userMedals.Contains(superStarMedal))
            {
                userMedals.Remove(superStarMedal);
            }
        }

        private async Task CheckSuperCommentatorMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var superCommenterMedal = await db.Medals.Get(5);

            var comments = db.Comments.Find(x => x.User.Id == user.Id).ToList();

            if (comments.Any(x=>x.Likes.Count >= 10) && !userMedals.Contains(superCommenterMedal))
            {
                userMedals.Add(superCommenterMedal);
            }
            else if (comments.Any(x => x.Likes.Count < 10) && userMedals.Contains(superCommenterMedal))
            {
                userMedals.Remove(superCommenterMedal);
            }
        }
    }
}
