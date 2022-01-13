using Data;
using Microsoft.AspNet.Identity.Owin;
using Shared;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CVGrupp37.Controllers
{
    [RoutePrefix("api/project")]
    public class BookApiController : ApiController
    {

        public ProjectRepository ProjectRepository
        {
            get { return new ProjectRepository(Request.GetOwinContext().Get<ApplicationDbContext>()); }
        }

        [HttpPost]   // Anropa med $.ajax eller $.post
        [Route("delete")] // api/project/delete
        public IHttpActionResult Delete(ProjectDeleteModel model)
        {
            try
            {
                var deletewasok = ProjectRepository.DeleteProject(model.ProjectId);
                if (deletewasok)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]   // Anropa med $.ajax eller $.post
        [Route("join")] // api/project/join
        public IHttpActionResult join(ProjectJoinModel model)
        {
            try
            {
                var joinwasok = ProjectRepository.JoinProject(model.ProjectId);
                if (joinwasok)
                {
                    return Ok();
                }


                return BadRequest();
            }
            catch
            {

                return BadRequest();
            }
        }

    }
}
