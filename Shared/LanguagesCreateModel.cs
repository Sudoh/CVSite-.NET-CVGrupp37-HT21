using Data;

namespace Shared
{
    public class LanguagesCreateModel
    {

        private readonly ApplicationDbContext _context;


        public LanguagesCreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public LanguagesCreateModel()
        {

        }

        public string Language { get; set; }

    }

    public class LanguagesEditModel
    {
        public int Id { get; set; }
        public string Language { get; set; }

    }


    public class LanguagesDeleteModel
    {
        public int LanguagesId { get; set; }
        public string Language { get; set; }
    }
}
