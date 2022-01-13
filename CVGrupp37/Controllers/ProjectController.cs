using Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;
using Shared;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project

        public ProjectService ProjectService
        {
            get { return new ProjectService(HttpContext.GetOwinContext().Get<ApplicationDbContext>()); }
        }

        public ProjectRepository ProjectRepository
        {
            get { return new ProjectRepository(HttpContext.GetOwinContext().Get<ApplicationDbContext>()); }
        }

        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var projects = context.Project.ToList();
                return View(projects);
            }

        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectCreateModel model)
        {

            try
            {
                var service = new ProjectService(System.Web.HttpContext.Current);
                var currentuser = User.Identity.GetUserId();
                if (currentuser != null)
                    service.SaveNewProject(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ProjectService.GetEditModel(id);
            return View(model);
        }

        // GET: Project/Edit/5
        public ActionResult ProjectDetails(int id)
        {
            var model = ProjectService.GetProjectDetailsModel(id);
            return View(model);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(ProjectEditModel model)
        {
            try
            {
                model = ProjectService.EditProject(model);
                ViewBag.Saved = true;
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var projects = context.Project.Where(x => x.Id == id).FirstOrDefault();
                    context.Project.Remove(projects);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
