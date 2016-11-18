using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<NewRatingModel>> AddRating(NewRatingModel model);

        IEnumerable<NewRatingModel> InitRatingModel(IEnumerable<Rating> ratings);
    }
}