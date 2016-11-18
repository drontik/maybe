using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    [Authorize]
    public class ChaptersController : BaseApiController
    {
        private readonly IChaptersService service;

        public ChaptersController(IChaptersService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("api/chapters/{chapterId}")]
        public async Task<IHttpActionResult> GetChapter(int chapterId)
        {
            return Ok(await service.GetChapter(chapterId));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/chapters")]
        public IHttpActionResult AddOrUpdateChapter(NewChapterModel model)
        {
            service.AddOrUpdateChapter(model);

            return Ok(model);
        }

        [HttpPost]
        [Route("api/chapters/delete/{chapterId}")]
        public async Task<IHttpActionResult> DeleteChapter(int chapterId)
        {
            var result = await service.DeleteChapter(chapterId);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/chapters/remember")]
        public async Task<IHttpActionResult> RememberChapter(RememberChapterModel model)
        {
            await service.SetRememberedChapter(model);

            return Ok();
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/chapters/getRememberedChapter")]
        public async Task<IHttpActionResult> GetRememberedChapter(RememberChapterModel model)
        {
            var result =  await service.GetRememberedChapter(model);

            return result != null ? Ok(result) : GetErrorResult(false);
        }
    }
}