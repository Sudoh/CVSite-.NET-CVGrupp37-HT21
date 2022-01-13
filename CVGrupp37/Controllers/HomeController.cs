using Data;
using Data.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class HomeController : Controller
    {
        //Väljer random publika CV och senaste skapade projekt att visa på startsidan.
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var randomCVs = context.CV.OrderBy(p => Guid.NewGuid()).ToList();
            var latestProjects = context.Project.OrderByDescending(x => x.DateAdded).ToList();
            var startView = new StartViewModel
            {
                startCVs = randomCVs,
                startProjects = latestProjects,
            };

            startView.startCVs.RemoveAll(x => x.ApplicationUser.Publik == false);
            return View(startView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}