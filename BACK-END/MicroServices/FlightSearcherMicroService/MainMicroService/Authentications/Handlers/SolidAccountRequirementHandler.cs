using System;
using System.Linq;
using System.Threading.Tasks;
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
            IHttpContextAccessor httpContextAccessor)
        {
          
            _httpContextAccessor = httpContextAccessor;
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

            context.Succeed(requirement);
        }

        #endregion

        #region Properties
        /// <summary>
        ///     Context accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion
    }
}