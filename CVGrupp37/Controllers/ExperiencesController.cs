using Data;
using Data.Repository;
using Services;
using Shared;
using System.Linq;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class ExperiencesController : Controller
    {
        public ExperiencesService ExperiencesService
        {
            get { return new ExperiencesService(); }
        }
        public ExperienceRepository ExperienceRepository
        {
            get { return new ExperienceRepository(); }
        }
        // GET: Experiences
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var experience = context.Experiences.ToList();
                return View(experience);
            }
        }
        // GET: Experiences/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Experiences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experiences/Create
        [HttpPost]
        public ActionResult Create(ExperienceCreateModel model)
        {
            try
            {
                var service = new ExperiencesService(System.Web.HttpContext.Current);
                service.SaveNewExperience(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Experiences/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ExperiencesService.GetEditModel(id);
            return View(model);
        }

        // POST: Experiences/Edit/5
        [HttpPost]
        public ActionResult Edit(ExperiencesEditModel model)
        {
            try
            {
                model = ExperiencesService.EditExperience(model);
                ViewBag.Saved = true;
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Experiences/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var experience = context.Experiences.Where(x => x.Id == id).FirstOrDefault();
                return View(experience);
            }
        }

        // POST: Experiences/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var experience = context.Experiences.Where(x => x.Id == id).FirstOrDefault();
                    context.Experiences.Remove(experience);
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