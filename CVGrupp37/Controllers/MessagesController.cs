using Data;
using Microsoft.AspNet.Identity;
using Shared;
using System.Linq;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class MessagesController : Controller
    {

        // GET: Messages
        [Authorize]
        public ActionResult Index()
        {

            var user = User.Identity.GetUserId();

            using (var context = new ApplicationDbContext())
            {
                var mess = context.Messages
                    .Where(x => x.ToUserID == user)
                    .OrderByDescending(d => d.DateSent)
                    .ToList();


                return View(mess);
            }

        }

        public static string GetAntalMessages()
        {

            var service = System.Web.HttpContext.Current;
            var userID = service.User.Identity.GetUserId();

            using (var context = new ApplicationDbContext())
            {
                var mess = context.Messages
                    .Where(x => x.ToUserID == userID)
                    .Where(x => x.IsRead == false)
                    .ToList();

                if (mess.Count > 0)
                {
                    return mess.Count().ToString();
                }

                return "";
            }


        }

        // GET: Messages/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {

                //Hämtar inloggad användares Id.

                var model = new CreateMessagesViewModel();
                model.From = User.Identity.GetUserId();


                using (var context = new ApplicationDbContext())
                {
                    //Matchar CvId mot id i databasen
                    var toUserIdfromCV = context.Users
                        .Where(x => x.CVid == id)
                        .ToList();

                    //Hämtar mottagarens Id
                    model.To = toUserIdfromCV[0].Id.ToString();

                    //om Model.From inte är null, så innehåller den ett Id.
                    if (!string.IsNullOrEmpty(model.From))
                    {
                        var FromUserNamn = context.Users
                       .Where(x => x.Id == model.From)
                       .ToList();

                        //byter ut avsvändarens id mot namn 
                        model.From = FromUserNamn[0].Namn.ToString();
                    }


                }
                // return PartialView("_MessageModal",model);
                return View(model);
            }

            return RedirectToAction("Index", "Home");

        }

    }
}
