using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ServiceShared.Interfaces.Services;
using ServiceShared.Models.CachedEntities;
using ServiceStack.OrmLite;

namespace ServiceShared.Services
{
    public class BaseProfileCacheService : IBaseProfileCacheService
    {
        #region Properties

        private readonly IDbConnection _dbConnection;

        #endregion

        #region Constructor

        public BaseProfileCacheService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual async Task<List<AccessToken>> FindAccessTokensByEmailAsync(string email)
        {
            return await _dbConnection.SelectAsync<AccessToken>(x => x.Email == email);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual async Task<AccessToken> FindAccessTokenByCodeAsync(string code)
        {
            return await _dbConnection.SingleAsync<AccessToken>(x => x.Code == code);
        }

        #endregion
    }
}