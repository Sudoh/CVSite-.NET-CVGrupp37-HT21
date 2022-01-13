using Data;
using Data.Repository;
using Services;
using Shared;
using System.Linq;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class EducationsController : Controller
    {
        public EducationsService EducationsService
        {
            get { return new EducationsService(); }
        }
        public EducationRepository EducationRepository
        {
            get { return new EducationRepository(); }
        }

        // GET: Educations
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var education = context.Educations.ToList();
                return View(education);
            }
        }

        // GET: Educations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Educations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Educations/Create
        [HttpPost]
        public ActionResult Create(EducationsCreateModel model)
        {
            try
            {
                var service = new EducationsService(System.Web.HttpContext.Current);
                service.SaveNewEducation(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Educations/Edit/5
        public ActionResult Edit(int id)
        {
            var model = EducationsService.GetEditModel(id);
            return View(model);
        }

        // POST: Educations/Edit/5
        [HttpPost]
        public ActionResult Edit(EducationsEditModel model)
        {
            try
            {
                model = EducationsService.EditEducation(model);
                ViewBag.Saved = true;
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Educations/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var education = context.Educations.Where(x => x.Id == id).FirstOrDefault();
                return View(education);
            }
        }

        // POST: Educations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var education = context.Educations.Where(x => x.Id == id).FirstOrDefault();
                    context.Educations.Remove(education);
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
