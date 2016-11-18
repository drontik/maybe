using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly ICommentsService service;

        public CommentsController(ICommentsService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/comments/{creativeId}")]
        public IHttpActionResult GetComments(int creativeId)
        {
            if (creativeId == 0)
            {
                return BadRequest("Creative Id is 0");
            }

            return Ok(service.GetComments(creativeId));
        }

        [Authorize]
        [HttpPost]
        [ValidateViewModel]
        public async Task<IHttpActionResult> AddComment(NewCommentModel model)
        {
            return Ok(await service.AddComment(model));
        }

        [Authorize]
        [HttpPost]
        [Route("api/comments/delete/{id}")]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            var result = await service.DeleteComment(id);

            return result != null ?  Ok(result) : GetErrorResult(false);
        }
    }
}

