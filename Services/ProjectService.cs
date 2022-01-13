using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Shared;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Services
{
    public class ProjectService
    {
        private readonly HttpContext _httpContext;
        private readonly ApplicationDbContext _context;

        private ProjectRepository ProjectRepository
        {
            get { return new ProjectRepository(_context ?? new ApplicationDbContext()); }
        }

        public ProjectService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public ProjectService() { }
        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProjectEditModel GetEditModel(int? id)
        {
            var model = new ProjectEditModel();
            if (id.HasValue)
            {
                var project = ProjectRepository.GetProject(id.Value);
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var userName = HttpContext.Current.User.Identity.Name;
                model.Title = project.Title;
                model.Description = project.Description;
                model.StartDate = project.StartDate;
                model.EndDate = project.EndDate;
                model.ProjectOwnerId = userId;
            }
            return model;
        }

        public Project GetProjectDetailsModel(int? id)
        {

            using (var context = new ApplicationDbContext())
            {
                var project = context.Project.Include(x => x.Participants).FirstOrDefault(x => x.Id == id);


                return project;
            }

        }

        public ProjectEditModel EditProject(ProjectEditModel model)
        {
            var project = ProjectRepository.GetProject(model.Id);

            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);

            project.Title = model.Title;
            project.Description = model.Description;
            project.StartDate = model.StartDate;
            project.EndDate = model.EndDate;

            ProjectRepository.SaveProjects(project);

            return model;
        }


        public void SaveNewProject(ProjectCreateModel model)
        {
            using (var context = new ApplicationDbContext())
            {

                var userId = HttpContext.Current.User.Identity.GetUserId();
                var userName = HttpContext.Current.User.Identity.Name;
                var newProject = new Project()

                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    DateAdded = DateTime.Now,
                    POName = userName,
                    ProjektOwnerId = userId

                };

                context.Project.Add(newProject);
                context.SaveChanges();

            }
        }
    }
}
