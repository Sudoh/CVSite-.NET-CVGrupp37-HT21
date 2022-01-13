using Data;
using Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Shared
{


    public class CurrentUserViewModel
    {

        private readonly ApplicationDbContext _context;


        public CurrentUserViewModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public CurrentUserViewModel()
        {

        }

        public string Namn { get; set; }

        [DisplayName("Bild")]
        public HttpPostedFileBase Image { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Skills> Skills { get; set; }
        public virtual ICollection<Educations> Educations { get; set; }

        public virtual ICollection<Experiences> Experiences { get; set; }
    }

    public class CurrentUserViewModellen
    {
        public int? CvId { get; set; }

    }




}
