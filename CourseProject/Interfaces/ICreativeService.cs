using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ICreativeService
    {
        Task<NewCreativeModel> GetCreativeById(int id);
        Task<NewCreativeModel> UpdateCreative(NewCreativeModel model);

        Task<IEnumerable<NewCreativeModel>> CreateCreative(NewCreativeModel model);
        Task<IEnumerable<NewCreativeModel>> DeleteCreative(int id);
        Task<IEnumerable<NewCreativeModel>> SearchCreatives(SearchViewModel model);
        Task<IEnumerable<NewCreativeModel>> GetUsersCreatives(string userName);

        IEnumerable<NewCreativeModel> GetAllCreatives();
        IEnumerable<NewCreativeModel> GetPartialCreatives(int delimiter);
        IEnumerable<NewCreativeModel> GetMostPopularCreatives();
        IEnumerable<NewCreativeModel> GetMostRatedCreatives();
        IEnumerable<NewCreativeModel> SearchCreativesByCategory(int categoryId);

        void Dispose(bool disposing);
    }
}