using System.Collections.Generic;
using System.Threading.Tasks;
using MainMicroService.Interfaces.Services;
using ServiceShared.Interfaces;
using ServiceShared.Models.CachedEntities;
using ServiceShared.Services;
using ServiceStack.OrmLite;

namespace MainMicroService.Services
{
    public class AppProfileCacheService : BaseProfileCacheService, IAppProfileCacheService
    {
        #region Properties

        /// <summary>
        ///     Access token db connection.
        /// </summary>
        private readonly IAccessTokenDbConnection _accessTokenDbConnection;

        #endregion

        #region Constructor

        public AppProfileCacheService(IAccessTokenDbConnection dbConnection) : base(dbConnection)
        {
            _accessTokenDbConnection = dbConnection;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override async Task<AccessToken> FindAccessTokenByCodeAsync(string code)
        {
            // Get current system time.
            return await _accessTokenDbConnection.SingleAsync<AccessToken>(x => x.Code == code);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public override async Task<List<AccessToken>> FindAccessTokensByEmailAsync(string email)
        {
            return await _accessTokenDbConnection.SelectAsync<AccessToken>(x => x.Email == email);
        }

        #endregion
    }
}