using Data;
using Data.Models;
using Data.Repository;
using Shared;
using System;
using System.Web;

namespace Services
{
    public class SkillsService
    {
        private HttpContext current;

        public SkillsService(HttpContext current)
        {
            this.current = current;
        }

        public SkillsService()
        {
        }

        private SkillsRepository SkillsRepository
        {
            get { return new SkillsRepository(); }
        }

        public SkillsEditModel GetEditModel(int id)
        {
            var skill = SkillsRepository.GetSkill(id);
            return new SkillsEditModel
            {
                Id = skill.Id,
                Skill = skill.Skill,

            };
        }


        public SkillsEditModel EditSkill(SkillsEditModel model)
        {
            var skill = SkillsRepository.GetSkill(model.Id);
            if (skill == null) throw new ArgumentException("Skill is not listed!");

            skill.Skill = model.Skill;
            SkillsRepository.SaveSkill(skill);

            return model;
        }

        public SkillsEditModel CreateNewSkill(SkillsEditModel model)
        {
            var newskill = new Skills()
            {
                Skill = model.Skill

            };

            SkillsRepository.SaveSkill(newskill);
            return model;
        }

        public void SaveNewSkill(SkillsCreateModel model)
        {
            using (var context = new ApplicationDbContext())
            {

                var newskill = new Skills()

                {
                    Skill = model.Skill,


                };

                context.Skills.Add(newskill);
                context.SaveChanges();

            }
        }
    }
}

