﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using AutoMapper;
using ClientShared.Enumerations;
using MainBusiness.Services;
using MainBusiness.Services.Implementations;
using MainBusiness.Services.Interfaces;
using MainDb.Interfaces;
using MainDb.Services;
using MainMicroService.Authentications.Handlers;
using MainMicroService.Authentications.Requirements;
using MainMicroService.Configs;
using MainMicroService.Constants;
using MainMicroService.Interfaces.Services;
using MainMicroService.Models;
using MainMicroService.Models.Captcha;
using MainMicroService.Services;
using MainModel.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using Serilog;
using ServiceShared.Extensions;
using ServiceShared.Interfaces.Services;
using ServiceShared.Models;
using ServiceShared.Services;
using VgySdk.Interfaces;
using VgySdk.Service;

namespace MainMicroService
{
    public class Startup
    {
        #region Properties

        /// <summary>
        ///     Instance stores configuration of application.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        ///     Hosting environement configuration.
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Callback which is fired when application starts.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile($"dbSetting.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // Set hosting environment.
            HostingEnvironment = env;
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services DI to app.
            AddServices(services);

            // Load jwt configuration from setting files.
            services.Configure<AppJwtModel>(Configuration.GetSection(MainConfigKeyConstant.AppJwt));
            
            // Cors configuration.
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.WithExposedHeaders("WWW-Authenticate");
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            // Add cors configuration to service configuration.
            services.AddCors(options => { options.AddPolicy("AllowAll", corsBuilder.Build()); });
            services.AddOptions();

            // Register jwt service.
            JsonWebTokenConfigs.Register(Configuration, services);

            // Add automaper configuration.
            services.AddAutoMapper(options => options.AddProfile(typeof(MappingProfile)));

            services.AddHttpClient();

            // Add swagger.
            services.AddSwaggerDocument();
            
            #region Mvc builder

            // Construct mvc options.
            services.AddMvc(mvcOptions =>
                {
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #endregion
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="serviceProvider"></param>
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            // Enable logging.
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            // Use in-app exception handler.
            app.UseCustomizedExceptionHandler(env);

            // Use strict transport security.
            app.UseHsts();

            // Use JWT Bearer authentication in the system.
            app.UseAuthentication();

            // Use https redirection.
            //app.UseHttpsRedirection();

            // Enable cors.
            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUi3();

            // Enable MVC features.
            app.UseMvc();
        }

        /// <summary>
        ///     Add dependency injection of services to app.
        /// </summary>
        private void AddServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            services.AddScoped<IBaseTimeService, BaseTimeService>();

            var skyScannerFlightSearchOptions = (SkyScannerFlightSearchConfiguration)Configuration.GetSection("skyScannerFlightSearchOption")
                .Get(typeof(SkyScannerFlightSearchConfiguration));

            if (skyScannerFlightSearchOptions != null)
                services.AddSingleton(skyScannerFlightSearchOptions);
            else
                services.AddSingleton<SkyScannerFlightSearchConfiguration>();

            services.AddHttpClient<ISkyScannerService, SkyScannerService>();
            services.AddScoped<ISkyScannerService, SkyScannerService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var skyScannerOptions = provider.GetService<SkyScannerFlightSearchConfiguration>();
                return new SkyScannerService(httpClientFactory.CreateClient(), skyScannerOptions);
            });
            //var sp = services.BuildServiceProvider();
            //var fooService = sp.GetService<ISkyScannerService>();
            // Add Swagger API document.
            services.AddSwaggerDocument(settings =>
            {
                var jsonSerializerSettings = new JsonSerializerSettings();
                jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.SerializerSettings = jsonSerializerSettings;

                settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("API Key"));
                settings.DocumentProcessors.Add(new SecurityDefinitionAppender("API Key",
                    new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = SwaggerSecurityApiKeyLocation.Header
                    }));
            });
        }

        #endregion
    }
}