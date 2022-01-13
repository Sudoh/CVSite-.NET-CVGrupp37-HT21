using Data;
using Data.Models;
using Data.Repository;
using Shared;
using System;
using System.Web;

namespace Services
{
    public class EducationsService
    {
        private HttpContext current;

        public EducationsService(HttpContext current)
        {
            this.current = current;
        }

        public EducationsService()
        {
        }

        private EducationRepository EducationRepository
        {
            get { return new EducationRepository(); }
        }

        public EducationsEditModel GetEditModel(int id)
        {
            var education = EducationRepository.GetEducation(id);
            return new EducationsEditModel
            {
                Id = education.Id,
                School = education.School,
                Education = education.Education,
                StartDate = education.StartDate,
                EndDate = education.EndDate

            };
        }

        public EducationsEditModel EditEducation(EducationsEditModel model)
        {
            var education = EducationRepository.GetEducation(model.Id);
            if (education == null) throw new ArgumentException("Education is not listed!");

            education.School = model.School;
            education.Education = model.Education;
            education.StartDate = model.StartDate;
            education.EndDate = model.EndDate;

            //education.Education = model.Education;
            EducationRepository.SaveEducations(education);

            return model;
        }

        public EducationsEditModel CreateNewEducation(EducationsEditModel model)
        {
            var neweducation = new Educations()
            {
                School = model.School,
                Education = model.Education,
                StartDate = model.StartDate,
                EndDate = model.EndDate

            };

            EducationRepository.SaveEducations(neweducation);
            return model;
        }

        public void SaveNewEducation(EducationsCreateModel model)
        {
            using (var context = new ApplicationDbContext())
            {

                var userName = HttpContext.Current.User.Identity.Name;
                var neweducation = new Educations()
                {
                    School = model.School,
                    Education = model.Education,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Creator = userName


                };

                context.Educations.Add(neweducation);
                context.SaveChanges();

            }
        }
    }
}
