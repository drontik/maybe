using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
     
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
   
        public string PhoneNumber { get; set; }

        public IEnumerable<MedalViewModel> Medals { get; set; }

        public string AvatarUri { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<IdentityUserRole> UserRoles { get; set; }
    }
}