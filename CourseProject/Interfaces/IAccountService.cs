using System.Net.Http;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Models;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Interfaces
{
    public interface IAccountService
    {
        Task<UserViewModel> GetUserInfo(string userName);
        Task<UserViewModel> UploadFile(HttpContent content);
        Task<IdentityResult> SaveUserData(UserViewModel viewModel);
        Task<IdentityResult> CreateUser(UserModel model);
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);

        UserViewModel InitUserViewModel(ApplicationUser user);
        ApplicationUser InitApplicationUser(UserViewModel model, ApplicationUser user);

        string WorkingFolder { get; }

        void Dispose(bool disposing);
    }
}