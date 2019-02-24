using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MainBusiness.Services.Interfaces;
using MainModel.Exceptions;
using MainModel.Models;
using MainModel.ViewModels;
using Newtonsoft.Json;

namespace MainBusiness.Services.Implementations
{
    public class SkyScannerService : ISkyScannerService
    {
        #region Properties

        /// <summary>
        /// Http client which is used for making requests.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Sky scanner flight search configuration.
        /// </summary>
        private readonly SkyScannerFlightSearchConfiguration options;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize service with injectors.
        /// </summary>
        /// <param name="httpClient"></param>
        public SkyScannerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Initialize service with injectors.
        /// </summary>
        public SkyScannerService(HttpClient httpClient, SkyScannerFlightSearchConfiguration options) : this(httpClient)
        {
            _httpClient.BaseAddress = new Uri(options.BaseUrl, UriKind.Absolute);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", options.ApiKey);
        }

        #endregion

        #region Methods

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> CreateFlightSearchSessionAsync(CreateFlightSearchSessionViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var modelDictionary = model.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(model, null)?.ToString());

            // Initialize http content.
            var httpContent = new FormUrlEncodedContent(modelDictionary);
            var httpResponseMessage =
                await _httpClient.PostAsync(new Uri("apiservices/pricing/v1.0", UriKind.Relative), httpContent, cancellationToken);

            // Read http response content.
            var httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var badRequestResponse = JsonConvert.DeserializeObject<SkyScannerBadRequestResponseViewModel>(httpResponseContent);
                throw new SkyScannerBadRequestException(badRequestResponse?.ValidationErrors);
            }

            // Ensure status is valid.
            httpResponseMessage.EnsureSuccessStatusCode();

            // Read the location header.
            var httpResponseHeaders = httpResponseMessage.Headers;
            if (httpResponseHeaders == null)
                throw new Exception("Header is empty.");

            if (httpResponseHeaders.Location == null)
                throw new Exception("Location is invalid");

            var locationUrl = httpResponseHeaders.Location.ToString();
            if (string.IsNullOrEmpty(locationUrl))
                throw new Exception("Location is not found");

            var sessionKey =
                locationUrl.Replace("http://partners.api.skyscanner.net/apiservices/pricing/uk1/v1.0/", "");

            if (string.IsNullOrEmpty(sessionKey))
                throw new Exception("Session key is not found");

            return sessionKey;
        }

        public async Task<ResultPricesViewModel> LoadFightInformationAsync(string session, CancellationToken cancellationToken = default(CancellationToken))
        {
            var httpResponseMessage =
                await _httpClient.GetAsync(new Uri($"apiservices/pricing/uk2/v1.0/{session}?pageIndex=0&pageSize=10&sortType=price&sortOrder=asc", UriKind.Relative), cancellationToken);

            var httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            // Ensure status is valid.
            httpResponseMessage.EnsureSuccessStatusCode();

            var model = JsonConvert.DeserializeObject<ResultPricesViewModel>(httpResponseContent);

            return model;
        }

        #endregion
    }
}