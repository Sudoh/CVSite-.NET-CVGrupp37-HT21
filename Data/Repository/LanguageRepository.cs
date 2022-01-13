using Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repository
{
    public class LanguageRepository
    {
        private ApplicationDbContext applicationDbContext;

        public LanguageRepository()
        {
        }

        public Languages GetLanguage(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Languages.FirstOrDefault(x => x.Id == id);
            }
        }

        public bool DeleteLanguage(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var language = context.Languages.FirstOrDefault(x => x.Id == id);
                if (language == null) return false;
                context.Languages.Remove(language);
                context.SaveChanges();
                return true;
            }
        }

        public List<Languages> GetAllLanguages()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Languages.ToList();
            }
        }

        public Languages SaveLanguage(Languages language)
        {
            using (var context = new ApplicationDbContext())
            {
                if (language.Id != 0)
                { //om har fått ett id, då finns boken redan, vi ska spara det som ändrats.
                    context.Entry(language).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                }
                else
                {
                    context.Languages.Add(language);
                }

                context.SaveChanges();
                return language;
            }
        }
    }
}
