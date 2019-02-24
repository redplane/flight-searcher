using System;
using System.Net.Http;
using System.Threading.Tasks;
using MainBusiness.Services.Implementations;
using MainBusiness.Services.Interfaces;
using MainModel.Models;
using MainModel.ViewModels;

namespace MainMicroServicePlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com", UriKind.Absolute);

            var skyScannerConfiguration = new SkyScannerFlightSearchConfiguration();
            skyScannerConfiguration.ApiKey = "a608d68391msh7d49e0b15616936p1f542cjsnce8e1906eef5";
            ISkyScannerService skyScannerService = new SkyScannerService(httpClient, skyScannerConfiguration);
            var sessionKey = CreateFlightSearchSessionAsync(skyScannerService).Result;

            Console.WriteLine($"Session key = {sessionKey}");
            Console.ReadLine();
        }

        /// <summary>
        /// Create flight search session.
        /// </summary>
        /// <param name="skyScannerService"></param>
        /// <returns></returns>
        public static Task<string> CreateFlightSearchSessionAsync(ISkyScannerService skyScannerService)
        {
            var model = new CreateFlightSearchSessionViewModel();
            model.OriginPlace = "KUL-sky";
            model.DestinationPlace = "SIN-sky";
            model.OutboundDate = new DateTime(2019, 2, 28).ToString("yyyy-MM-dd");
            var sessionKey = skyScannerService.CreateFlightSearchSessionAsync(model);
            return sessionKey;
        }
    }
}
