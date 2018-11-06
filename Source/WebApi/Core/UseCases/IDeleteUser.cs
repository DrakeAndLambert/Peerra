using System.Threading.Tasks;

namespace DrakeLambert.Peerra.WebApi.Core.UseCases
{
    public interface IDeleteUser
    {
        Task Handle(string username, string password);
    }
}