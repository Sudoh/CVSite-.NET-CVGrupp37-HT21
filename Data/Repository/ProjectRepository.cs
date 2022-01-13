using Data.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Data
{
    public class ProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Project GetProject(int id)
        {
            using (var context = new ApplicationDbContext())
            {

                return context.Project
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public bool DeleteProject(int id)
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

        public bool JoinProject(int id)
        {
            using (var context = new ApplicationDbContext())
            {

                var project = context.Project.FirstOrDefault(x => x.Id == id);
                var currentuser = HttpContext.Current.User.Identity.GetUserId();
                var user = context.Users.FirstOrDefault(x => x.Id == currentuser);
                if (project == null) return false;

                if (project != null && user != null)


                    user.Projects.Add(project);
                project.Participants.Add(user);

                context.Entry(user).State = EntityState.Modified;
                context.Entry(project).State = EntityState.Modified;

                context.SaveChanges();
                return true;
            }
        }



        public List<Project> GetAllProjects()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Project
                    .Include(x => x.Participants)
                    .ToList();
            }
        }

        public Project SaveProjects(Project project)
        {
            using (var context = new ApplicationDbContext())
            {
                if (project.Id != 0)
                { //om har fått ett id, då finns projektet redan, vi ska spara det som ändrats.
                    context.Entry(project).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                }
                else
                {

                    context.Project.Add(project);
                }

                context.SaveChanges();
                return project;
            }
        }
        public ApplicationUser SaveUser(ApplicationUser user)
        {
            using (var context = new ApplicationDbContext())
            {
                if (user.Id != null)
                { //om har fått ett id, då finns användaren redan, vi ska spara det som ändrats.
                    context.Entry(user).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                }
                else
                {

                    context.Users.Add(user);
                }

                context.SaveChanges();
                return user;
            }
        }
    }
}

