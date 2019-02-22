using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceShared.Models.CachedEntities;

namespace ServiceShared.Interfaces.Services
{
    public interface IBaseProfileCacheService
    {
        #region Methods

        /// <summary>
        ///     Get user by access token from cache.
        /// </summary>
        /// <returns></returns>
        Task<List<AccessToken>> FindAccessTokensByEmailAsync(string email);

        /// <summary>
        ///     Find access token by access token code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AccessToken> FindAccessTokenByCodeAsync(string code);

        #endregion
    }
}