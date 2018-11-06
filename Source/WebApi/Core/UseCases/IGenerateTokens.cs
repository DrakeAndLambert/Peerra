using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.Core.UseCases
{
    public interface IGenerateTokens
    {
        Task<(AccessToken, RefreshToken)> Handle(string username, string password, string ipAddress);
    }
}