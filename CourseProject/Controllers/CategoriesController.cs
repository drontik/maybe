using System.Collections.Generic;
using System.Web.Http;
using CourseProject.Domain.Entities;

using CourseProject.Interfaces;

namespace CourseProject.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService service;

        public CategoriesController(ICategoriesService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/categories")]
        public IEnumerable<CreativeCategory> GetCategories()
        {
            return service.GetCategories();
        }
    }
}