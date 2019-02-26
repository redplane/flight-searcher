using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainBusiness.Services.Interfaces;
using MainModel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceShared.Interfaces.Services;

namespace MainMicroService.Controllers
{
    [Route("api/flight")]
    [AllowAnonymous]
    public class FlightController : Controller
    {
        #region Properties

        private readonly ISkyScannerService _skyScannerService;

        private readonly IBaseTimeService _baseTimeService;
        #endregion
        #region Constructor

        public FlightController(ISkyScannerService skyScannerService, IBaseTimeService baseTimeService)
        {
            _skyScannerService = skyScannerService;

            _baseTimeService = baseTimeService;
        }

        #endregion

        #region Methods
        
        [HttpGet("")]
        public async Task<IActionResult> GetCheapestPriceArrangeDate(double fromDateUnix, double toDateUnix)
        {
            var pricesResult = new List<PriceDetailViewModel>();
            var fromdate = _baseTimeService.UnixToDateTimeUtc(fromDateUnix);
            var todate = _baseTimeService.UnixToDateTimeUtc(toDateUnix);
            var totalDay = (todate - fromdate).TotalDays;
            var tasksSection = new List<Task<string>>();

            for (int i = 0; i <= totalDay; i++)
            {
                var dateFlight = fromdate.AddDays(i);

                var pricedate = new PriceDetailViewModel();
                //Set date
                pricedate.DateFight = _baseTimeService.DateTimeUtcToUnix(dateFlight);

                //Get price
                tasksSection.Add(Task.Run(() => CreateFlightSearchSessionAsync( dateFlight.ToLocalTime())));

                pricesResult.Add(pricedate);

            }

            var sessions = await Task.WhenAll(tasksSection);

            var taskprice = new List<Task<ResultPricesViewModel>>();
            foreach (var result in sessions)
            {
                taskprice.Add(Task.Run(() => _skyScannerService.LoadFightInformationAsync(result)));

            }

            var resultPricesViewModels = await Task.WhenAll(taskprice);

            int k = 0;
            foreach (var result in resultPricesViewModels)
            {
                var cheapestPrice = GetCheapestPrice(result);
                if (cheapestPrice != null)
                {
                    pricesResult[k].Price = cheapestPrice.Price;
                }
                k++;
            }
            return Ok(pricesResult);
        }

        public  async Task<string> CreateFlightSearchSessionAsync( DateTime dateFlight)
        {
            var model = new CreateFlightSearchSessionViewModel();
            model.OriginPlace = "KUL-sky";
            model.DestinationPlace = "SIN-sky";
            model.OutboundDate = dateFlight.ToString("yyyy-MM-dd");
            var sessionKey = await _skyScannerService.CreateFlightSearchSessionAsync(model);
            return sessionKey;
        }

        public  PricingOption GetCheapestPrice(ResultPricesViewModel allPirces)
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
        #endregion
    }
}