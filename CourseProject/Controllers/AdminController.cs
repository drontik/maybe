using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Models;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly IAdminService service;
        private readonly ICreativeService creativeService;
        private readonly IAccountService authService;

        public AdminController(IAdminService service, ICreativeService creativeService, IAccountService authService)
        {
            this.service = service;
            this.creativeService = creativeService;
            this.authService = authService;
        }

        [HttpGet]
        [Route("api/admin/users")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(service.GetUsers());
        }

        [HttpGet]
        [Route("api/admin/medals")]
        public IHttpActionResult GetAllMedals()
        {
            return Ok(service.GetMedals());
        }

        [HttpGet]
        [Route("api/admin/ratings")]
        public IHttpActionResult GetAllRatings()
        {
            return Ok(service.GetRatings());
        }

        [HttpGet]
        [Route("api/admin/comments")]
        public IHttpActionResult GetAllComments()
        {
            return Ok(service.GetComments());
        }

        [HttpGet]
        [Route("api/admin/tags")]
        public IHttpActionResult GetAllTags()
        {
            return Ok(service.GetTags());
        }

        [HttpGet]
        [Route("api/admin/chapters")]
        public IHttpActionResult GetAllChapters()
        {
            return Ok(service.GetChapters());
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/category")]
        public async Task<IHttpActionResult> SaveCategory(NewCategoryModel model)
        {
            return Ok(await service.SaveCategory(model));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/saveComment")]
        public async Task<IHttpActionResult> SaveComment(NewCommentModel model)
        {
            return Ok(await service.SaveComment(model));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/saveRating")]
        public async Task<IHttpActionResult> SaveRating(NewRatingModel model)
        {
            return Ok(await service.SaveRating(model));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/saveTag")]
        public async Task<IHttpActionResult> SaveTag(TagsViewModel model)
        {
            return Ok(await service.SaveTag(model));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/save")]
        public async Task<IHttpActionResult> SaveUserData(UserViewModel model)
        {
            return Ok(await service.SaveUserData(model));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/delete")]
        public async Task<IHttpActionResult> DeleteUser(UserViewModel model)
        {
            var result = GetErrorResult(await service.DeleteUser(model));

            return result ?? Ok(service.GetUsers());
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/reset")]
        public async Task<IHttpActionResult> ResetPasword(ResetPasswordModel model)
        {
            var result = GetErrorResult(await service.ResetPassword(model));

            return result ?? Ok(service.GetUsers());
        }

        [HttpPost]
        [Route("api/admin/deleteCreative/{id}")]
        public async Task<IHttpActionResult> DeleteCreative(int id)
        {
            await creativeService.DeleteCreative(id);

            return Ok(creativeService.GetAllCreatives());
        }

        [HttpPost]
        [Route("api/admin/updateCreative")]
        public async Task<IHttpActionResult> UpdateCreative(NewCreativeModel model)
        {
            await creativeService.UpdateCreative(model);

            return Ok(creativeService.GetAllCreatives());
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/deleteTag/{id}")]
        public async Task<IHttpActionResult> DeleteTag(int id)
        {
            var result = await service.DeleteTag(id);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/register")]
        public async Task<IHttpActionResult> Register(UserModel model)
        {
            var errorResult = GetErrorResult(await authService.CreateUser(model));

            return errorResult ?? Ok(service.GetUsers());
        }
       
        [HttpPost]
        [Route("api/admin/deleteRating/{id}")]
        public async Task<IHttpActionResult> DeleteRating(int id)
        {
            var result = await service.DeleteRating(id);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/deleteComment/{id}")]
        public async Task<IHttpActionResult> DeleteComment(int id)
          {
            var result = await service.DeleteComment(id);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [HttpPost]
        [Route("api/admin/chapters/delete/{chapterId}")]
        public async Task<IHttpActionResult> DeleteChapter(int chapterId)
        {
            var result = await service.DeleteChapter(chapterId);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [HttpPost]
        [Route("api/admin/category/{categoryId}")]
        public async Task<IHttpActionResult> DeleteCategory(int categoryId)
        {
            var result = await service.DeleteCategory(categoryId);

            return result != null ? Ok(result) : GetErrorResult(false);
        }
    }
}
