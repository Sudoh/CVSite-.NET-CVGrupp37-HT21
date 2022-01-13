using Data;
using Data.Repository;
using Services;
using Shared;
using System.Linq;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class SkillsController : Controller
    {
        public SkillsService SkillsService
        {
            get { return new SkillsService(); }
        }
        public SkillsRepository SkillsRepository
        {
            get { return new SkillsRepository(); }
        }

        // GET: Skills
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var skills = context.Skills.ToList();
                return View(skills);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        [HttpPost]
        public ActionResult Create(SkillsCreateModel model)
        {
            try
            {
                var service = new SkillsService(System.Web.HttpContext.Current);
                service.SaveNewSkill(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int id)
        {
            var model = SkillsService.GetEditModel(id);
            return View(model);
        }

        // POST: Skills/Edit/5
        [HttpPost]
        public ActionResult Edit(SkillsEditModel model)
        {
            try
            {
                model = SkillsService.EditSkill(model);
                ViewBag.Saved = true;
                return View(model);
            }
            catch
            {
                return View();
            }
        }


        // GET: Skills/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var skill = context.Skills.Where(x => x.Id == id).FirstOrDefault();
                return View(skill);
            }
        }

        // POST: Skills/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var skill = context.Skills.Where(x => x.Id == id).FirstOrDefault();
                    context.Skills.Remove(skill);
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
