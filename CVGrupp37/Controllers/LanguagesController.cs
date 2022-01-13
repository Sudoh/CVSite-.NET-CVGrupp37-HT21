using Data;
using Data.Repository;
using Services;
using Shared;
using System.Linq;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class LanguagesController : Controller
    {
        public LanguagesService LanguagesService
        {
            get { return new LanguagesService(); }
        }
        public LanguageRepository LanguageRepository
        {
            get { return new LanguageRepository(); }
        }

        // GET: Languages
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var language = context.Languages.ToList();
                return View(language);
            }
        }

        // GET: Languages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        [HttpPost]
        public ActionResult Create(LanguagesCreateModel model)
        {
            try
            {
                var service = new LanguagesService(System.Web.HttpContext.Current);
                service.SaveNewLanguages(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Languages/Edit/5
        public ActionResult Edit(int id)
        {
            var model = LanguagesService.GetEditModel(id);
            return View(model);
        }

        // POST: Languages/Edit/5
        [HttpPost]
        public ActionResult Edit(LanguagesEditModel model)
        {
            try
            {
                model = LanguagesService.EditLanguage(model);
                ViewBag.Saved = true;
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Languages/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var language = context.Languages.Where(x => x.Id == id).FirstOrDefault();
                return View(language);
            }
        }

        // POST: Languages/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var language = context.Languages.Where(x => x.Id == id).FirstOrDefault();
                    context.Languages.Remove(language);
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
