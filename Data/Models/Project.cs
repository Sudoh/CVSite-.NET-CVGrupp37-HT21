using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public DateTime DateAdded { get; set; }
        public string ProjektOwnerId { get; set; }
        public string POName { get; set; }
        public virtual ICollection<ApplicationUser> Participants { get; set; }

    }
}
