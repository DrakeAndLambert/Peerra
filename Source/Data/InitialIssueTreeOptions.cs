using System.Linq;
using DrakeLambert.Peerra.Entities;

namespace DrakeLambert.Peerra.Data
{
    public class InitialIssueTreeOptions
    {
        public IssueOption[] InitialIssueTree { get; set; }
    }

    public class IssueOption
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IssueOption[] Children { get; set; }

        public static implicit operator Issue(IssueOption issueOption)
        {
            return new Issue(issueOption.Title, issueOption.Description, issueOption.Children?.Select(io => (Issue)io));
        }
    }
}
