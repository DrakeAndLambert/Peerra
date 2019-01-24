using System;
using System.Linq;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Pages.Issues
{
	public class PeerModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public Issue Issue { get; set; }

		public PeerModel(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
		{
			_context = context;
			_signInManager = signInManager;
		}

		public async System.Threading.Tasks.Task<IActionResult> OnGetAsync(Guid id)
		{
			Issue = await _context.Issues
				.Include(i => i.HelpRequests)
					.ThenInclude(hr => hr.Helper)
				.Include(i => i.Owner)
				.Include(i => i.Topic)
				.SingleOrDefaultAsync(i => i.Id == id);

			if (Issue == null)
			{
				return RedirectToPage("/NotFound", new { message = "This issue may have been removed or moved." });
			}

			if (_signInManager.IsSignedIn(User))
			{
				var user = await _signInManager.UserManager.GetUserAsync(User);
				if (user.Id == Issue.OwnerId)
				{
					return RedirectToPage("/Issues/Single", new { id = id });
				}
			}
			
			return Page();
		}
	}
}
