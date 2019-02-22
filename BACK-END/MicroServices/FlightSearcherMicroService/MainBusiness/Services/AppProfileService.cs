using MainBusiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ServiceShared.Models;
using ServiceShared.Services;

namespace MainBusiness.Services
{
    public class AppProfileService : BaseProfileService, IAppProfileService
    {
        #region Constructor

        public AppProfileService(IOptions<AppJwtModel> appJwt, IHttpContextAccessor httpContextAccessor) : base(appJwt,
            httpContextAccessor)
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}