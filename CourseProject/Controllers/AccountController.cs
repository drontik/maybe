using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Models;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [ValidateViewModel]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel model)
        {
            var errorResult = GetErrorResult(await service.CreateUser(model));

            return errorResult ?? Ok();
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("info/{userName}")]
        public async Task<IHttpActionResult> GetUserInfo(string userName)
        {
            if (userName == null)
            {
                return BadRequest("User name is null");
            }

            var result = await service.GetUserInfo(userName);

            return result != null ? Ok(result) : GetErrorResult(false);
        }

        [Authorize]
        [HttpPost]
        [ValidateViewModel]
        [Route("saveInfo")]
        public async Task<IHttpActionResult> SaveUserInfo(UserViewModel model)
        {
            var errorResult = GetErrorResult(await service.SaveUserData(model)); 

            return errorResult ?? Ok(await service.GetUserInfo(model.UserName));
        }

        [Authorize]
        [HttpPost]
        [ValidateViewModel]
        [Route("changePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            var errorResult = GetErrorResult(await service.ChangePassword(model));

            return errorResult ?? Ok(await service.GetUserInfo(model.UserName));
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose(disposing);

            base.Dispose(disposing);
        }
    }
}