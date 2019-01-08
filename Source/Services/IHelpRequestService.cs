using System.Threading.Tasks;
using DrakeLambert.Peerra.Entities;

namespace DrakeLambert.Peerra.Services
{
    public interface IHelpRequestService
    {
        Task RequestHelpAsync(Issue issue);
    }
}
