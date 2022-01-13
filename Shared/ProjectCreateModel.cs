using Data;
using System;
using System.Collections.Generic;

namespace Shared
{


    public class ProjectCreateModel
    {

        private readonly ApplicationDbContext _context;


        public ProjectCreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProjectCreateModel()
        {

        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateAdded { get; set; }

        public string ProjectOwnerId { get; set; }

        public string POName { get; set; }
    }

    public class ProjectEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ProjectOwnerId { get; set; }

        public ICollection<ApplicationUser> Participants { get; set; }

    }

    public class ProjectDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateAdded { get; set; }

        public string ProjectOwnerId { get; set; }

        public ICollection<ApplicationUser> Participants { get; set; }

        public string POName { get; set; }


    }


    public class ProjectDeleteModel
    {
        public int ProjectId { get; set; }
    }

    public class ProjectJoinModel
    {
        public int ProjectId { get; set; }
    }
}
