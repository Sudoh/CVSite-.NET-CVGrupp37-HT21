using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Shared
{


    public class CVCreateModel
    {

        private readonly ApplicationDbContext _context;


        public CVCreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public CVCreateModel()
        {

        }
        public int Id { get; set; }

        public string Namn { get; set; }

        [DisplayName("Bild")]
        public HttpPostedFileBase Image { get; set; }

        public string ExistingImagePath { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int[] ChosenSkills { get; set; }

        public int[] ChosenEducations { get; set; }

        public int[] ChosenExperiences { get; set; }



        public List<SkillEditKeyValueViewModel> Skills { get; set; }

        public List<SkillEditKeyValueViewModel> Educations { get; set; }

        public List<SkillEditKeyValueViewModel> Experiences { get; set; }


    }

    public class SkillEditKeyValueViewModel
    {
        public int Id { get; set; }


        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        public string Education { get; set; }


        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
        public string Name { get; set; }
    }

    public class CVEditModel
    {
        public int Id { get; set; }
        public string Namn { get; set; }

        [DisplayName("Bild")]
        public HttpPostedFileBase Image { get; set; }

        public string ExistingImagePath { get; set; }


        public int[] ChosenSkills { get; set; }
        public virtual ICollection<Skills> Skills { get; set; }
        public virtual ICollection<Educations> Educations { get; set; }

        public virtual ICollection<Experiences> Experiences { get; set; }

    }

    public class CVDetailModel
    {


        public int Id { get; set; }
        public string Namn { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Skills> Skills { get; set; }
        public virtual ICollection<Educations> Educations { get; set; }

        public virtual ICollection<Experiences> Experiences { get; set; }


    }




}
