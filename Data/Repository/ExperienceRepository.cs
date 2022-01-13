using Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Data.Repository
{
    public class ExperienceRepository
    {
        public ExperienceRepository(ApplicationDbContext applicationDbContext)
        {
        }

        public ExperienceRepository()
        {
        }

        public Experiences GetExperience(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Experiences.FirstOrDefault(x => x.Id == id);
            }
        }

        public bool DeleteExperience(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var experience = context.Experiences.FirstOrDefault(x => x.Id == id);
                if (experience == null) return false;
                context.Experiences.Remove(experience);
                context.SaveChanges();
                return true;
            }
        }

        public List<Experiences> GetAllExperiences()
        {
            using (var context = new ApplicationDbContext())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                return context.Experiences.Where(x => x.Creator.Equals(userName)).ToList();
            }
        }

        public Experiences SaveExperiences(Experiences experience)
        {
            using (var context = new ApplicationDbContext())
            {
                if (experience.Id != 0)
                { //om har fått ett id, då finns boken redan, vi ska spara det som ändrats.
                    context.Entry(experience).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                }
                else
                {
                    context.Experiences.Add(experience);
                }

                context.SaveChanges();
                return experience;
            }
        }
    }
}
