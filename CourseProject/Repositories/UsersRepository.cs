using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Repositories
{
    class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
  
        public UsersRepository(DatabaseContext db)
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public async Task<ApplicationUser> FindUserById(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(string userId, string pass, string newPass)
        {
            return await userManager.ChangePasswordAsync(userId, pass, newPass);
        }

        public async Task<IdentityResult> ResetPassword(string userId, string newPass)
        {
            var result = await userManager.RemovePasswordAsync(userId);

            if (result.Succeeded)
            {
                return await userManager.AddPasswordAsync(userId, newPass);
            }
            return result;
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {

            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                JoinDate = DateTime.Now
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return userManager.Users.ToList();
        }

        public async Task<ApplicationUser> FindUser(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            return await userManager.FindAsync(userName, password);
        }

        public async Task<bool> CheckUserRole(string userId)
        {
            return await userManager.IsInRoleAsync(userId, "Admin");
        }

        public async Task<IdentityResult> DeleteUser(ApplicationUser user)
        {
            return await userManager.DeleteAsync(user);
        }
    }
}
