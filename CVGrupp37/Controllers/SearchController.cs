using Data;
using System.Linq;
using System.Web.Mvc;

namespace CVGrupp37.Controllers
{
    public class SearchController : Controller
    {


        // GET: Search
        public ActionResult Index(string searching)
        {

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (var context = new ApplicationDbContext())
                    {

                        return View(context.Users.Where(x => x.Namn.Contains(searching) || searching == null).ToList());

                    }

                }
                else
                {
                    using (var context = new ApplicationDbContext())
                    {
                        return View(context.Users.Where(x => x.Namn.Contains(searching) && x.Publik == true || searching == null).ToList());
                    }


                }

            }
            catch
            {
                return View();
            }
            ModelState.Clear();


        }
    }
}