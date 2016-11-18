using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime JoinDate { get; set; }

        public string AvatarUri { get; set; }

        public List<ChapterStore> ChapterReadStore { get; set; }

        public virtual ICollection<Medal> Medals { get; set; }
    }
}