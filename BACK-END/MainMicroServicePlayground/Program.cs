using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MainBusiness.Services.Implementations;
using MainBusiness.Services.Interfaces;
using MainModel.Models;
using MainModel.ViewModels;
using ServiceShared.Interfaces.Services;
using ServiceShared.Services;

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
            //string sessionKey = String.Empty;
            //var getSession = new Task(() =>
            //{
            //    sessionKey=  CreateFlightSearchSessionAsync(skyScannerService).Result;
            //    Console.WriteLine($"Session key = {sessionKey}");
            //});
            //getSession.Start();

            //Task.WhenAll(getSession).Wait();

            //Get cheapest price
            //var fightInfos= new ResultPricesViewModel();
            //var getfileInfo = new Task(() =>
            //{
            //    fightInfos =  GetPricesAsync(skyScannerService, sessionKey).Result;
            //});

            //getfileInfo.Start();

            //Task.WhenAll(getfileInfo).Wait();


            //var cheapestPrice = GetCheapestPrice(fightInfos);

            //Console.WriteLine(cheapestPrice.Price);

            //Get cheapest from arrange date
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var priceArr = GetCheapestPriceForArrangeDate(1551369600000, 1553875200000, skyScannerService);

            timer.Stop();
            Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", timer.Elapsed);

            foreach (var priceInfo in priceArr)
            {
                Console.WriteLine(priceInfo.Price);
                Console.WriteLine(priceInfo.DateFight);
            }
            Console.ReadLine();

        }

        /// <summary>
        /// Create flight search session.
        /// </summary>
        /// <param name="skyScannerService"></param>
        /// <returns></returns>
        public static async Task<string> CreateFlightSearchSessionAsync(ISkyScannerService skyScannerService, DateTime dateFlight)
        {
            var model = new CreateFlightSearchSessionViewModel();
            model.OriginPlace = "KUL-sky";
            model.DestinationPlace = "SIN-sky";
            model.OutboundDate = dateFlight.ToString("yyyy-MM-dd");
            var sessionKey = await skyScannerService.CreateFlightSearchSessionAsync(model);
            return sessionKey;
        }

        public static async Task<ResultPricesViewModel> GetPricesAsync(ISkyScannerService skyScannerService, string sessionKey)
        {

            var cheapestPrice = await skyScannerService.LoadFightInformationAsync(sessionKey);
            return cheapestPrice;
        }

        public static List<PriceDetailViewModel> GetCheapestPriceForArrangeDate(double fromDateUnix, double toDateUnix, ISkyScannerService skyScannerService)
        {
            IBaseTimeService timeService = new BaseTimeService();
            var pricesResult = new List<PriceDetailViewModel>();
            var fromdate = timeService.UnixToDateTimeUtc(fromDateUnix);
            var todate = timeService.UnixToDateTimeUtc(toDateUnix);
            var totalDay = (todate - fromdate).TotalDays;
            var tasksSection = new List<Task<string>>();

            for (int i = 0; i <= totalDay; i++)
            {
                var dateFlight = fromdate.AddDays(i);

                var pricedate = new PriceDetailViewModel();
                //Set date
                pricedate.DateFight = timeService.DateTimeUtcToUnix(dateFlight);

                //Get price
                tasksSection.Add(Task.Run(() => CreateFlightSearchSessionAsync(skyScannerService, dateFlight.ToLocalTime())));

                pricesResult.Add(pricedate);

            }

            var continuation = Task.WhenAll(tasksSection);
            try
            {
                continuation.Wait();
            }
            catch (AggregateException)
            { }

            if (continuation.Status == TaskStatus.RanToCompletion)
            {
                var taskprice = new List<Task<ResultPricesViewModel>>();
                foreach (var result in continuation.Result)
                {
                    taskprice.Add(Task.Run(() => GetPricesAsync(skyScannerService, result)));

                }

                var continuationPrice = Task.WhenAll(taskprice);
                try
                {
                    continuationPrice.Wait();
                }
                catch (AggregateException)
                { }

                if (continuationPrice.Status == TaskStatus.RanToCompletion)
                {
                    int i = 0;
                    foreach (var result in continuationPrice.Result)
                    {
                        var cheapestPrice = GetCheapestPrice(result);
                        if (cheapestPrice != null)
                        {
                            pricesResult[i].Price = cheapestPrice.Price;
                        }
                        i++;
                    }
                }
            }
            // Display information on faulted tasks.
            else
            {
                foreach (var t in tasksSection)
                    Console.WriteLine("Task {0}: {1}", t.Id, t.Status);
            }

            return pricesResult;
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

            return cheapestPrice;

        }
    }
}
