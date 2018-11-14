using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi2.IdentityCore.Entities;
using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi2.IdentityCore.Interfaces.Infrastructure
{
    public interface IRefreshTokenRepository
    {
        Task<List<RefreshToken>> GetRefreshTokensAsync(string username);

        Task AddRefreshTokenAsync(RefreshToken refreshToken);

        Task<Result> DeleteRefreshTokenAsync(Guid id);
    }
}
