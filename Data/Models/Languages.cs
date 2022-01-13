using System.Collections.Generic;

namespace Data.Models
{
    public class Languages
    {
        public int Id { get; set; }
        public string Language { get; set; }

        public virtual ICollection<CV> CV { get; set; }
    }
}
