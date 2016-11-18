using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Filters;
using CourseProject.Interfaces;

namespace CourseProject.Controllers
{

    public class TagsController : ApiController
    {
        private readonly ITagsService tagService;

        public TagsController(ITagsService tagServ)
        {
            tagService = tagServ;
        }

        [HttpGet]
        [Route("api/tags")]
        public IHttpActionResult GetAllTags()
        {
            return Ok(tagService.GetAllTags());
        }

        [HttpGet]
        [Route("api/tags/{creativeId}")]
        public async Task<IHttpActionResult> GetCreativeTags(int creativeId)
        {
            if (creativeId == 0)
            {
                return BadRequest("Creative Id is 0");
            }

            return Ok(await tagService.GetCreativeTags(creativeId));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/tags/{creativeId}")]
        public IHttpActionResult SaveTags(int creativeId, IEnumerable<Tag> model)
        {
            return Ok(tagService.SaveTags(creativeId, model));
        }
    }
}
