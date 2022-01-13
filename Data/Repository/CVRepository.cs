using Data.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Data
{
    public class CVRepository
    {
        private readonly ApplicationDbContext _context;

        public CVRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CV GetCV(int id)
        {


            return _context.CV.Include(x => x.ApplicationUser)
                 .Include("ApplicationUser.Projects")
                 .Include(x => x.Skills)
                 .Include(x => x.Educations)
                 .Include(x => x.Experiences)
                 .Where(x => x.Id == id)
                 .FirstOrDefault();

        }

        public bool DeleteCV(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var project = context.Project.FirstOrDefault(x => x.Id == id);
                var currentuser = HttpContext.Current.User.Identity.GetUserId();
                if (project == null) return false;

                if (project != null)
                    context.Project.Remove(project);
                context.SaveChanges();
                return true;
            }
        }


        public List<CV> GetAllCVs()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.CV
                    .Include(x => x.ApplicationUser)
                    .ToList();
            }
        }

        public CV SaveCVs(CV cv)
        {
            using (var context = new ApplicationDbContext())
            {
                if (cv.Id != 0)
                { //om har fått ett id, då finns projektet redan, vi ska spara det som ändrats.
                    context.Entry(cv).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                }
                else
                {

                    context.CV.Add(cv);
                }

                context.SaveChanges();
                return cv;
            }
        }
        public CV SaveCV(CV cv)
        {

            if (cv.Id != null)
            { //om har fått ett id, då finns projektet redan, vi ska spara det som ändrats.
                _context.Entry(cv).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
            }
            else
            {

                _context.CV.Add(cv);
            }

            _context.SaveChanges();
            return cv;

        }
    }
}

