using DrakeLambert.Peerra.Entities;

namespace DrakeLambert.Peerra.Data
{
    public class InitialDataSeed
    {
        private readonly ApplicationDbContext _context;

        public InitialDataSeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Issues.RemoveRange(_context.Issues);

            foreach (var issue in Issues)
            {
                // issue.Description = _fillerText;
            }

            _context.Issues.AddRange(Issues);
            _context.SaveChanges();
        }

        private const string _fillerText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam mi elit, pretium quis massa vehicula, placerat convallis erat. Quisque mattis.";

        private readonly Issue[] Issues =
        {
            new Issue("Academic Life", "When you're having trouble dealing with courses, professors, and more.", new[]
            {
                new Issue("Course Work"),
                new Issue("Scheduling"),
                new Issue("Professors"),
            }),
            new Issue("Campus Life", "On campus housing, dining, transportation, and the like.", new[]
            {
                new Issue("Housing")
            }),
            new Issue("Off Campus Life", "Careers, local restaurants, and everything in between.", new[]
            {
                new Issue("Careers")
            }),
            new Issue("University Affairs", "Financial aid, clubs... really anything outside of class.", new[]
            {
                new Issue("Scholarships")
            }),
            new Issue("Something Else", "When your problem doesn't fit anywhere else.", new[]
            {
                new Issue("VOID")
            })
        };

        private readonly string[] TopLevelItems =
        {
            "Academic Life",
            "Campus Life",
            "Off Campus Life",
            "University Affairs",
            "Something Else"
        };
    }
}
/*

Knowing when and how to schedule advising appointments
finding interesting easy a electives
good and bad professors


            "A Professor",
            "Choosing Courses",
            "Choosing Majors",
            "Residential Life",
 */
