using DrakeLambert.Peerra.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace RazorPagesIntro.Pages
{
    public class IndexModel2 : PageModel
    {
        public IEnumerable<Connection> Connections { get; set; }

        public void OnGet()
        {
            Connections = new[] {
                new Connection {

                }
            };
        }
    }
}