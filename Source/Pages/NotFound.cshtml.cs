using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrakeLambert.Peerra.Pages
{
    public class NotFoundModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet([FromQuery] string message)
        {
            Message = message;
        }
    }
}
