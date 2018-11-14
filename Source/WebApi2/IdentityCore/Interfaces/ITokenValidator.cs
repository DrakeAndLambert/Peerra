using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi2.IdentityCore.Interfaces
{
    public interface ITokenValidator
    {
        Result<string> ValidateAndGetUser(string accessToken);
    }
}
