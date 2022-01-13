using System.Collections.Generic;

namespace Data.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public string Skill { get; set; }
        public virtual ICollection<CV> CV { get; set; }


    }
}
