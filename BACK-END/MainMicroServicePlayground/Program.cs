using System;
using System.Collections.Generic;
using System.Linq;
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
            //var httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com", UriKind.Absolute);

            //var skyScannerConfiguration = new SkyScannerFlightSearchConfiguration();
            //skyScannerConfiguration.ApiKey = "a608d68391msh7d49e0b15616936p1f542cjsnce8e1906eef5";
            //ISkyScannerService skyScannerService = new SkyScannerService(httpClient, skyScannerConfiguration);
            //var sessionKey = CreateFlightSearchSessionAsync(skyScannerService).Result;

            //Console.WriteLine($"Session key = {sessionKey}");

            //Get cheapest price

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com", UriKind.Absolute);

            var skyScannerConfiguration = new SkyScannerFlightSearchConfiguration();
            skyScannerConfiguration.ApiKey = "a608d68391msh7d49e0b15616936p1f542cjsnce8e1906eef5";
            ISkyScannerService skyScannerService = new SkyScannerService(httpClient, skyScannerConfiguration);
            var fightInfos = GetPricesAsync(skyScannerService).Result;

            var cheapestPrice = GetCheapestPrice(fightInfos);

            Console.WriteLine(cheapestPrice.Price);

            Console.WriteLine(cheapestPrice.DeeplinkUrl);

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

        public static Task<ResultPricesViewModel> GetPricesAsync(ISkyScannerService skyScannerService)
        {
            var sessionKey = "492dab1e-e411-4fb9-a9a3-e06aa147cf8b";
            var cheapestPrice = skyScannerService.LoadFightInformationAsync(sessionKey);
            return cheapestPrice;
        }

        public static PricingOption GetCheapestPrice(ResultPricesViewModel allPirces)
        {
            var pricingOptions = new List<PricingOption>();
            
            var itineraries = allPirces.Itineraries;
            foreach (var itinerary in itineraries)
            {
                var cheapestPriceOption = itinerary.PricingOptions.FirstOrDefault();
                pricingOptions.Add(cheapestPriceOption);

            }

            var cheapestPrice = pricingOptions.OrderBy(c => c.Price).FirstOrDefault();
            if(cheapestPrice == null)
                throw new Exception();

            var cheapestPrices = pricingOptions.Where(c => c.Price == cheapestPrice.Price);


            return cheapestPrice;

        }
    }
}
