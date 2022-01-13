using Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Namn { get; set; }
        public string Adress { get; set; }
        public bool Publik { get; set; }
        public int? CVid { get; set; }

        public virtual ICollection<Project> Projects { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Project> Project { get; set; }

        public DbSet<Skills> Skills { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Experiences> Experiences { get; set; }
        public DbSet<Educations> Educations { get; set; }

        public DbSet<CV> CV { get; set; }

        //public DbSet<ApplicationUser> Participants { get; set; }

        public DbSet<Messages> Messages { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
