using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Domain.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        Task<ApplicationUser> FindUser(string userName);
        Task<ApplicationUser> FindUser(string userName, string password);
        Task<ApplicationUser> FindUserById(string userId);

        Task<IdentityResult> UpdateUser(ApplicationUser user);
        Task<IdentityResult> DeleteUser(ApplicationUser user);

        Task<IdentityResult> RegisterUser(UserModel userModel);

        Task<IdentityResult> ChangePassword(string userId, string pass, string newPass);
        Task<IdentityResult> ResetPassword(string userId, string newPass);
       
        Task<bool> CheckUserRole(string userId);
    }
}
