using Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repository
{
    public class SkillsRepository
    {

        public SkillsRepository(ApplicationDbContext applicationDbContext)
        {
        }

        public SkillsRepository()
        {
        }


        public Skills GetSkill(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Skills.FirstOrDefault(x => x.Id == id);
            }
        }

        public bool Deleteskill(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var skill = context.Skills.FirstOrDefault(x => x.Id == id);
                if (skill == null) return false;
                context.Skills.Remove(skill);
                context.SaveChanges();
                return true;
            }
        }

        public List<Skills> GetAllSkills()
        {
            using (var context = new ApplicationDbContext())
            {

                return context.Skills.ToList();
            }
        }

        public Skills SaveSkill(Skills skill)
        {
            using (var context = new ApplicationDbContext())
            {
                if (skill.Id != 0)
                { //om har fått ett id, då finns boken redan, vi ska spara det som ändrats.
                    context.Entry(skill).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                }
                else
                {
                    context.Skills.Add(skill);
                }

                context.SaveChanges();
                return skill;
            }
        }
    }
}
