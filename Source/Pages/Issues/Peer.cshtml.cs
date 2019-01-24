using System;
using System.Linq;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrakeLambert.Peerra.Pages.Issues
{
	public class PeerModel : PageModel
	{
		public Issue Issue { get; set; }

		public IActionResult OnGet(Guid id)
		{
			Issue = new Issue
			{
				Title = "How does banker's rounding work?",
				Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic",
				HelpRequests = Enumerable.Range(0, 5)
					.Select(i => new HelpRequest
					{
						Message = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
						Status = HelpRequestStatus.Responded,
						Helper = new ApplicationUser
						{
							UserName = (new[] { "Bob", "Sally", "Billy T" })[i % 3],
							Email = "email@email.com"
						}
					}
				).ToList(),
				Topic = new Topic
				{
					Title = "Accounting"
				},
				Owner = new ApplicationUser
				{
					UserName = "Thomas"
				}
			};
			Issue.HelpRequests.ForEach(hr => hr.Issue = Issue);
			return Page();
		}
	}
}
