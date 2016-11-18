using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;

namespace CourseProject.Interfaces
{
    public interface IMedalService
    {
       Task<ICollection<Medal>> CheckMedals(ApplicationUser user);
    }
}