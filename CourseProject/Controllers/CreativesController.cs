using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class CreativesController : BaseApiController
    {
        private readonly ICreativeService service;

        public CreativesController(ICreativeService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateViewModel]
        [Route("api/creatives/search")]
        public async Task<IHttpActionResult> Search(SearchViewModel model)
        {
            return Ok(await service.SearchCreatives(model));
        }

        [AllowAnonymous]
        [HttpGet]
        [ValidateViewModel]
        [Route("api/creatives/searchByCategory/{categoryId}")]
        public IHttpActionResult SearchByCategory(int categoryId)
        {
            return Ok(service.SearchCreativesByCategory(categoryId));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/creatives/getall")]
        public IHttpActionResult GetAllCreatives()
        {
           return Ok(service.GetAllCreatives());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/creatives/getall/{username}")]
        public async Task<IHttpActionResult> GetCreatives(string userName)
        {
            if (userName == null) return BadRequest("User name is null");

            var result = await service.GetUsersCreatives(userName);

            return result != null ? Ok(result) : GetErrorResult(false);
        }


        [HttpGet]
        [Route("api/creatives/{id}")]
        public async Task<IHttpActionResult> GetCreative(int id)
        {
            if (id == 0) return BadRequest("Creative Id is 0");

            var result = await service.GetCreativeById(id);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [HttpPost]
        [Authorize]
        [Route("api/creatives/delete/{id}")]
        public async Task<IHttpActionResult> DeleteCreative(int id)
        {
            var result = await service.DeleteCreative(id);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [HttpPost]
        [Authorize]
        [ValidateViewModel]
        [Route("api/creatives")]
        public async Task<IHttpActionResult> CreateCreative(NewCreativeModel model)
        {
            return Ok(await service.CreateCreative(model));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/creatives/update")]
        public async Task<IHttpActionResult> UpdateCreative(NewCreativeModel model)
        {
           return Ok(await service.UpdateCreative(model));
        }

        [HttpGet]
        [Route("api/creatives/getPartial/{delimiter}")]
        public IHttpActionResult GetPartialCreatives(int delimiter)
        {
            return Ok(service.GetPartialCreatives(delimiter));
        }

        [HttpGet]
        [Route("api/creatives/popular")]
        public IHttpActionResult GetMostPopularCreatives()
        {
            return Ok(service.GetMostPopularCreatives());
        }

        [HttpGet]
        [Route("api/creatives/rated")]
        public IHttpActionResult GetMostRatedCreatives()
        {
            return Ok(service.GetMostRatedCreatives());
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose(disposing);

            base.Dispose(disposing);
        }
    }
}