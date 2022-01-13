using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CV
    {
        [Key]
        public int Id { get; set; }
        public string Namn { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Skills> Skills { get; set; }
        public virtual ICollection<Educations> Educations { get; set; }

        public virtual ICollection<Experiences> Experiences { get; set; }


    }
}
