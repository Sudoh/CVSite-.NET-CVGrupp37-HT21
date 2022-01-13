using Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;
using Shared;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace CVGrupp37.Controllers
{
    public class CVController : Controller
    {


        public CVService CVService
        {
            get { return new CVService(HttpContext.GetOwinContext().Get<ApplicationDbContext>()); }
        }
        // GET: CV
        public ActionResult Index()
        {

            using (var context = new ApplicationDbContext())
            {
                var currentuser = User.Identity.GetUserId();
                var user = context.Users.FirstOrDefault(x => x.Id == currentuser);
                var model = new CurrentUserViewModellen();
                if (user.CVid != null)
                {
                    model.CvId = user.CVid;


                }
                return View(model);
            }


        }


        public ActionResult Details(int id)
        {
            var projektmodel = new ProjectCreateModel();

            var model = new ViewModelsCVSite();
            model.CvModel = CVService.GetCVDetailsModel(id);
            model.ProjectModel = projektmodel;
            return View(model);
        }



        // GET: CV/Create
        public ActionResult Create()
        {
            var model = CVService.GetCreateModel();
            return View("Create", model);
        }

        // POST: CV/Create
        [HttpPost]
        public ActionResult Create(CVCreateModel model)
        {
            try
            {


                model = CVService.CreateNewCV(model);

                MessageBox.Show("You have now created your CV");

                return View(model);



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
                MessageBox.Show("You have to be logged in to create a project");
            }
        }

        // GET: CV/Edit/5
        public ActionResult Edit(int id)
        {
            var model = CVService.GetCvEditModel(id);

            return View(model);
        }

        // POST: CV/Edit/5
        [HttpPost]
        public ActionResult Edit(CVCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                CVService.FillCreateModelWithAssociations(model);
                return View(model);
            }
            try
            {
                model = CVService.EditCV(model);
                CVService.FillCreateModelWithAssociations(model);
                MessageBox.Show("You have now Edited your CV");
                return View(model);
            }
            catch
            {
                return View();
            }
        }

    }
}
