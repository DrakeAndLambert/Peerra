using System.Threading.Tasks;

namespace DrakeLambert.Peerra.WebApi.Core.UseCases
{
    public interface IDeleteUser
    {
        Task HandleAsync(string username, string password);
    }
}