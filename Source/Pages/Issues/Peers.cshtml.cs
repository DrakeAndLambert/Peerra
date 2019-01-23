using System.Collections.Generic;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrakeLambert.Peerra.Pages.Issues
{
	public class PeersModel : PageModel
	{
		private readonly Topic _fakeTopic = new Topic
		{
			Title = "Accounting"
		};

		public List<Issue> TargetedIssues { get; set; } = new List<Issue> {
			new Issue {
				Title = "How Does Banker's Rounding Work?",
				Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
			},
			new Issue {
				Title = "How Does Banker's Rounding Work?",
				Description = "Description, How Does Banker's Rounding Work?",
			},
			new Issue {
				Title = "How Does Banker's Rounding Work?",
				Description = "Description, How Does Banker's Rounding Work?",
			},
			new Issue {
				Title = "How Does Banker's Rounding Work?",
				Description = "Description, How Does Banker's Rounding Work?",
			}
		};

		public PeersModel()
		{
			TargetedIssues.ForEach(issue => { issue.Topic = _fakeTopic; });
		}

		public List<Issue> AllIssues => TargetedIssues;

		public IActionResult OnGet()
		{
			return Page();
		}
	}
}
