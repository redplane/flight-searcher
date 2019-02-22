using System;
using System.Linq;
using System.Threading.Tasks;
using MainBusiness.Interfaces;
using MainMicroService.Authentications.Requirements;
using MainMicroService.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ServiceShared.Authentications.ActionFilters;

namespace MainMicroService.Authentications.Handlers
{
    public class SolidAccountRequirementHandler : AuthorizationHandler<SolidAccountRequirement>
    {
        #region Constructor

        /// <summary>
        ///     Initiate requirement handler with injectors.
        /// </summary>
        public SolidAccountRequirementHandler(
            IAppProfileService profileService, IAppProfileCacheService appProfileCacheService,
            IHttpContextAccessor httpContextAccessor)
        {
            _profileService = profileService;
            _httpContextAccessor = httpContextAccessor;
            _appProfileCacheService = appProfileCacheService;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handle requirement asychronously.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SolidAccountRequirement requirement)
        {
            // Convert authorization filter context into authorization filter context.
            var authorizationFilterContext = (AuthorizationFilterContext) context.Resource;

            // Get user access token.
            var requestAccessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            try
            {
                // Get access token from cache.
                //var account = _profileCacheService.Read(iId);
                var accessToken = await _appProfileCacheService.FindAccessTokenByCodeAsync(requestAccessToken);

                if (accessToken == null)
                    throw new Exception("Cannot find user information from cache service");
                
                // TODO: Validate user role.
                context.Succeed(requirement);
            }
            catch
            {
                if (authorizationFilterContext == null)
                {
                    context.Fail();
                    return;
                }

                // Method or controller authorization can be by passed.
                if (authorizationFilterContext.Filters.Any(x => x is ByPassAuthorizationAttribute))
                {
                    _profileService.BypassAuthorizationFilter(context, requirement);
                    return;
                }

                context.Fail();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Provides functions to access service which handles identity businesses.
        /// </summary>
        private readonly IAppProfileService _profileService;

        private readonly IAppProfileCacheService _appProfileCacheService;

        /// <summary>
        ///     Context accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion
    }
}