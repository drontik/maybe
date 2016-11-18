using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    [Authorize]
    public class RatingsController : ApiController
    {
        private readonly IRatingService service;

        public RatingsController(IRatingService serv)
        {
            service = serv;
        }
       
        [HttpPost]
        [ValidateViewModel]
        [Route("api/rating")]
        public async Task<IHttpActionResult> AddRating(NewRatingModel model)
        {
            var result = await service.AddRating(model);

            if (result == null)
            {
                return BadRequest("Bad request");
            }

            return Ok(result);
        }
    }
}
