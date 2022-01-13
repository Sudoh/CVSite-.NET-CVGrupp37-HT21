using System.Collections.Generic;

namespace Data.Models
{
    public class StartViewModel
    {
        public List<CV> startCVs { get; set; }
        public List<Project> startProjects { get; set; }

        public ApplicationUser User { get; set; }

        //public ny upload

    }
}
