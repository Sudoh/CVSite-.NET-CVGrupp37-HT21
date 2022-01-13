using Data;
using System;

namespace Shared
{
    public class EducationsCreateModel
    {

        private readonly ApplicationDbContext _context;


        public EducationsCreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public EducationsCreateModel()
        {

        }
        public String School { get; set; }
        public String Education { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public String Creator { get; set; }

    }
    public class EducationsEditModel
    {
        public int Id { get; set; }
        public String School { get; set; }
        public String Education { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class EducationsDeleteModel
    {
        public int EducationsId { get; set; }
    }
}
