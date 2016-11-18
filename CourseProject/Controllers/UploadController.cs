using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Interfaces;

namespace CourseProject.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IAccountService service;

        public UploadController(IAccountService service)
        {
            this.service = service;
        }
 
        [HttpDelete]
        public IHttpActionResult Delete(string fileName)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("api/upload")]
        public async Task<IHttpActionResult> Add()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            try
            {

                return Ok(await service.UploadFile(Request.Content));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}
