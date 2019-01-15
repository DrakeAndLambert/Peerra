using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Entities;

namespace DrakeLambert.Peerra.Services
{
    public interface IUserIssueService
    {
        Task<List<Issue>> GetImportantIssuesAsync(ApplicationUser user);

        Task<List<Issue>> GetOthersTargetedIssuesAsync(ApplicationUser user);

        Task<List<Issue>> GetOthersIssuesAsync(ApplicationUser user);
    }
}
