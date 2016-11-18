using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace CourseProject.Controllers
{
    public class BaseApiController : ApiController
    {
        public IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        public IHttpActionResult GetErrorResult(bool result)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            return BadRequest(ModelState);

        }
    }
}