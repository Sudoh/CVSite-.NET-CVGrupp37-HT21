using Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Data.Repository
{
    public class EducationRepository
    {
        public EducationRepository(ApplicationDbContext applicationDbContext)
        {
        }

        public EducationRepository()
        {
        }

        public Educations GetEducation(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Educations.FirstOrDefault(x => x.Id == id);
            }
        }

        public bool DeleteEducation(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var education = context.Educations.FirstOrDefault(x => x.Id == id);
                if (education == null) return false;
                context.Educations.Remove(education);
                context.SaveChanges();
                return true;
            }
        }

        public List<Educations> GetAllEducations()
        {
            using (var context = new ApplicationDbContext())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                return context.Educations.Where(x => x.Creator.Equals(userName)).ToList();
            }
        }

        public Educations SaveEducations(Educations education)
        {
            using (var context = new ApplicationDbContext())
            {
                if (education.Id != 0)
                { //om har fått ett id, då finns boken redan, vi ska spara det som ändrats.
                    context.Entry(education).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                }
                else
                {
                    context.Educations.Add(education);
                }

                context.SaveChanges();
                return education;
            }
        }
    }
}
