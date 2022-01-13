using Data;
using Data.Models;
using Data.Repository;
using Shared;
using System;
using System.Web;

namespace Services
{
    public class ExperiencesService
    {
        private HttpContext current;

        public ExperiencesService(HttpContext current)
        {
            this.current = current;
        }

        public ExperiencesService()
        {
        }

        private ExperienceRepository ExperienceRepository
        {
            get { return new ExperienceRepository(); }
        }

        public ExperiencesEditModel GetEditModel(int id)
        {


            var experience = ExperienceRepository.GetExperience(id);
            return new ExperiencesEditModel
            {
                Id = experience.Id,
                WorkPlace = experience.WorkPlace,
                Text = experience.Text,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate

            };
        }

        public ExperiencesEditModel EditExperience(ExperiencesEditModel model)
        {
            var experience = ExperienceRepository.GetExperience(model.Id);
            if (experience == null) throw new ArgumentException("Education is not listed!");

            experience.WorkPlace = model.WorkPlace;
            experience.Text = model.Text;
            experience.StartDate = model.StartDate;
            experience.EndDate = model.EndDate;

            //education.Education = model.Education;
            ExperienceRepository.SaveExperiences(experience);

            return model;
        }

        public ExperiencesEditModel CreateNewEducation(ExperiencesEditModel model)
        {
            var userName = HttpContext.Current.User.Identity.Name;
            var newexperience = new Experiences()
            {
                WorkPlace = model.WorkPlace,
                Text = model.Text,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Creator = userName

            };

            ExperienceRepository.SaveExperiences(newexperience);
            return model;
        }

        public void SaveNewExperience(ExperienceCreateModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var newexperience = new Experiences()
                {
                    WorkPlace = model.WorkPlace,
                    Text = model.Text,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Creator = userName


                };

                context.Experiences.Add(newexperience);
                context.SaveChanges();

            }
        }
    }
}
