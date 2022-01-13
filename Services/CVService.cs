using Data;
using Data.Models;
using Data.Repository;
using Microsoft.AspNet.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Services
{
    public class CVService
    {
        private readonly HttpContext _httpContext;
        private readonly ApplicationDbContext _context;

        private CVRepository CVRepository
        {
            get { return new CVRepository(_context ?? new ApplicationDbContext()); }
        }

        private SkillsRepository SkillsRepository
        {
            get { return new SkillsRepository(_context ?? new ApplicationDbContext()); }
        }

        private EducationRepository EducationRepository
        {
            get { return new EducationRepository(_context ?? new ApplicationDbContext()); }
        }

        private ExperienceRepository ExperienceRepository
        {
            get { return new ExperienceRepository(_context ?? new ApplicationDbContext()); }
        }

        public CVService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public CVService() { }
        public CVService(ApplicationDbContext context)
        {
            _context = context;
        }

        private ImageService ImageService
        {
            get { return new ImageService(); }
        }

        public CVCreateModel GetCreateModel()
        {
            var model = new CVCreateModel();

            FillCreateModelWithAssociations(model);

            return model;
        }

        public CVCreateModel GetCvEditModel(int? id)
        {
            var model = new CVCreateModel();
            if (id.HasValue)
            {
                var cv = CVRepository.GetCV(id.Value);
                model.Id = cv.Id;
                model.Namn = cv.Namn;
                model.ExistingImagePath = cv.ImagePath;

                model.ChosenSkills = cv.Skills.Select(x => x.Id).ToArray();
                model.ChosenEducations = cv.Educations.Select(x => x.Id).ToArray();
                model.ChosenExperiences = cv.Experiences.Select(x => x.Id).ToArray();

            }

            FillCreateModelWithAssociations(model);

            return model;
        }

        public CVDetailModel GetCVDetailsModel(int? id)
        {

            var model = new CVDetailModel();
            if (id.HasValue)
            {
                var cv = CVRepository.GetCV(id.Value);


                model.Id = cv.Id;
                model.Namn = cv.Namn;
                model.Email = cv.ApplicationUser.Email;
                model.ImagePath = cv.ImagePath;
                model.ApplicationUser = cv.ApplicationUser;

                model.Skills = cv.Skills;
                model.Educations = cv.Educations;
                model.Experiences = cv.Experiences;


            }
            return model;
        }


        public CVCreateModel FillCreateModelWithAssociations(CVCreateModel model)
        {
            var skills = SkillsRepository.GetAllSkills();
            var educations = EducationRepository.GetAllEducations();
            var experiences = ExperienceRepository.GetAllExperiences();

            model.Skills = skills.Select(x => new SkillEditKeyValueViewModel { Name = x.Skill, Id = x.Id }).ToList();
            model.Educations = educations.Select(x => new SkillEditKeyValueViewModel { Name = x.School, startDate = x.StartDate, endDate = x.EndDate, Education = x.Education, Id = x.Id }).ToList();
            model.Experiences = experiences.Select(x => new SkillEditKeyValueViewModel { Name = x.WorkPlace, Id = x.Id }).ToList();

            return model;
        }

        public CVCreateModel EditCV(CVCreateModel model)
        {


            var cv = CVRepository.GetCV(model.Id);
            if (cv == null) throw new ArgumentException("CV finns inte!");
            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(cv.ImagePath)) ImageService.RemoveImageFromDiskIfExists(cv.ImagePath); //gör lite cleanup
                cv.ImagePath = ImageService.SaveImageToDisk(model.Image);
            }
            model.ExistingImagePath = cv.ImagePath;

            cv.Namn = model.Namn;

            // Remove skills before adding
            if (cv.Skills != null)
            {
                var toRemoveSkills = new List<Skills>();
                foreach (var skills in cv.Skills)
                {
                    if (model.ChosenSkills == null || !model.ChosenSkills.Contains(skills.Id))
                        toRemoveSkills.Add(skills);
                }

                foreach (var skills in toRemoveSkills)
                {
                    cv.Skills.Remove(skills);
                }
            }

            //we will add new skills that wasn't chosen before
            if (model.ChosenSkills != null)
            {
                if (cv.Skills == null) cv.Skills = new List<Skills>();
                foreach (var skillsid in model.ChosenSkills.Where(skillsid => cv.Skills.All(x => x.Id != skillsid)))
                {
                    var skills = _context.Skills.FirstOrDefault(x => x.Id == skillsid);
                    if (skills != null)
                    {
                        cv.Skills.Add(skills);
                    }
                }
            }


            // Remove educations before adding
            if (cv.Educations != null)
            {
                var toRemoveEducations = new List<Educations>();
                foreach (var education in cv.Educations)
                {
                    if (model.ChosenEducations == null || !model.ChosenEducations.Contains(education.Id))
                        toRemoveEducations.Add(education);
                }

                foreach (var education in toRemoveEducations)
                {
                    cv.Educations.Remove(education);
                }
            }

            // we will add educations that wasn't chosen before.
            if (model.ChosenEducations != null)
            {
                if (cv.Educations == null) cv.Educations = new List<Educations>();
                foreach (var educationid in model.ChosenEducations.Where(educationid => cv.Educations.All(x => x.Id != educationid)))
                {
                    var education = _context.Educations.FirstOrDefault(x => x.Id == educationid);
                    if (education != null)
                    {
                        cv.Educations.Add(education);
                    }
                }
            }

            // Remove educations before adding
            if (cv.Experiences != null)
            {
                var toRemoveExperiences = new List<Experiences>();
                foreach (var experiences in cv.Experiences)
                {
                    if (model.ChosenExperiences == null || !model.ChosenExperiences.Contains(experiences.Id))
                        toRemoveExperiences.Add(experiences);
                }

                foreach (var experiences in toRemoveExperiences)
                {
                    cv.Experiences.Remove(experiences);
                }
            }

            //vi we will add experiences that wasn't chosen before.
            if (model.ChosenExperiences != null)
            {
                if (cv.Experiences == null) cv.Experiences = new List<Experiences>();
                foreach (var experienceid in model.ChosenExperiences.Where(experienceid => cv.Experiences.All(x => x.Id != experienceid)))
                {
                    var experiences = _context.Experiences.FirstOrDefault(x => x.Id == experienceid);
                    if (experiences != null)
                    {
                        cv.Experiences.Add(experiences);
                    }
                }
            }


            _context.Entry(cv).State = EntityState.Modified;
            _context.SaveChanges();
            return model;

        }

        [Authorize]
        public CVCreateModel CreateNewCV(CVCreateModel model)
        {
            using (var context = new ApplicationDbContext())
            {

                var currentuser = HttpContext.Current.User.Identity.GetUserId();
                var user = context.Users.FirstOrDefault(x => x.Id == currentuser);

                var newCV = new CV()
                {
                    Namn = model.Namn,
                    ApplicationUser = user,
                    Skills = new List<Skills>(),
                    Educations = new List<Educations>(),
                    Experiences = new List<Experiences>()
                };

                if (user.CVid == null)
                {
                    if (model.Image != null)
                    {
                        newCV.ImagePath = ImageService.SaveImageToDisk(model.Image);
                    }

                    if (model.ChosenSkills != null)
                    {
                        foreach (var skillid in model.ChosenSkills)
                        {
                            var skill = context.Skills.FirstOrDefault(x => x.Id == skillid);
                            if (skill != null)
                            {
                                newCV.Skills.Add(skill);
                            }
                        }
                    }

                    if (model.ChosenEducations != null)
                    {
                        foreach (var educationid in model.ChosenEducations)
                        {
                            var education = context.Educations.FirstOrDefault(x => x.Id == educationid);
                            if (education != null)
                            {
                                newCV.Educations.Add(education);
                            }
                        }
                    }

                    if (model.ChosenExperiences != null)
                    {
                        foreach (var experienceid in model.ChosenExperiences)
                        {
                            var experience = context.Experiences.FirstOrDefault(x => x.Id == experienceid);
                            if (experience != null)
                            {
                                newCV.Experiences.Add(experience);
                            }
                        }
                    }

                    context.CV.Add(newCV);
                    context.SaveChanges();
                    // Below adds ID from new CV to property in ApplicationsUser.
                    int cvid = newCV.Id;
                    user.CVid = cvid;

                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                }
                return model;

            }
        }
    }
}
