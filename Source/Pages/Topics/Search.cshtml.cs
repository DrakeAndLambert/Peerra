using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Pages.Topics
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SearchModel> _logger;

        public List<Topic> Results { get; set; }

        [BindProperty]
        public string Search { get; set; }

        public SearchModel(ApplicationDbContext context, ILogger<SearchModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                var search = Search.ToUpper();
                _logger.LogInformation("Searching for {query}.", search);
                Results = await _context.Topics.Where(topic =>
                    topic.Title.ToUpper().Contains(search) ||
                    topic.Description.ToUpper().Contains(search)
                ).ToListAsync();
                _logger.LogInformation("Found {count} results.", Results.Count);
            }
        }

        public void OnGet()
        { }
    }
}
