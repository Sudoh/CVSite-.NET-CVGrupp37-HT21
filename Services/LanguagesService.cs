using Data;
using Data.Models;
using Data.Repository;
using Shared;
using System;
using System.Web;

namespace Services
{
    public class LanguagesService
    {
        private HttpContext current;

        public LanguagesService(HttpContext current)
        {
            this.current = current;
        }

        public LanguagesService()
        {
        }

        private LanguageRepository LanguageRepository
        {
            get { return new LanguageRepository(); }
        }

        public LanguagesEditModel GetEditModel(int id)
        {
            var languages = LanguageRepository.GetLanguage(id);
            return new LanguagesEditModel
            {
                Id = languages.Id,
                Language = languages.Language,

            };
        }

        public LanguagesEditModel EditLanguage(LanguagesEditModel model)
        {
            var language = LanguageRepository.GetLanguage(model.Id);
            if (language == null) throw new ArgumentException("Language is not listed!");


            language.Language = model.Language;
            LanguageRepository.SaveLanguage(language);

            return model;
        }

        public LanguagesEditModel CreateNewLanguage(LanguagesEditModel model)
        {
            var newlanguage = new Languages()
            {
                Language = model.Language

            };

            LanguageRepository.SaveLanguage(newlanguage);
            return model;
        }

        public void SaveNewLanguages(LanguagesCreateModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var newlanguage = new Languages()

                {
                    Language = model.Language


                };

                context.Languages.Add(newlanguage);
                context.SaveChanges();

            }
        }
    }
}
