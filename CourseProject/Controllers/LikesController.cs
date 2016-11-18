using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    [Authorize]
    public class LikesController : ApiController
    {
        private readonly ILikesService service;

        public LikesController(ILikesService serv)
        {
            service = serv;
        }

        [HttpGet]
        [Route("api/likes/{id}")]
        public async Task<IHttpActionResult> GetLikes(int commentId)
        {
            if (commentId == 0) return BadRequest("Comment Id is null");

            return Ok(await service.GetLikes(commentId));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/likes")]
        public async Task<IHttpActionResult> AddLike(NewLikeModel model)
        {
            return Ok(await service.AddLike(model));
        }
    }
}
