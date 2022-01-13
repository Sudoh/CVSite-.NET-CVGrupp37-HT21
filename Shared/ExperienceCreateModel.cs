using Data;
using System;

namespace Shared
{
    public class ExperienceCreateModel
    {
        private readonly ApplicationDbContext _context;


        public ExperienceCreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public ExperienceCreateModel()
        {

        }
        public String WorkPlace { get; set; }
        public String Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public String Creator { get; set; }

    }
    public class ExperiencesEditModel
    {
        public int Id { get; set; }
        public String WorkPlace { get; set; }
        public String Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class ExperiencesDeleteModel
    {
        public int ExperiencesId { get; set; }
    }
}

