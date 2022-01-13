using Data;

namespace Shared
{


    public class SkillsCreateModel
    {

        private readonly ApplicationDbContext _context;


        public SkillsCreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public SkillsCreateModel()
        {

        }

        public string Skill { get; set; }

    }

    public class SkillsEditModel
    {
        public int Id { get; set; }
        public string Skill { get; set; }

    }


    public class SkillsDeleteModel
    {
        public int SkillsId { get; set; }
    }


}
