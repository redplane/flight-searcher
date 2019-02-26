using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainBusiness.Services.Interfaces;
using MainMicroService.ViewModels;
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

        /// <summary>
        /// Service for searching flights.
        /// </summary>
        private readonly ISkyScannerService _skyScannerService;

        /// <summary>
        /// Service for handling system time.
        /// </summary>
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

        [HttpPost("search")]
        public async Task<IActionResult> FindCheapestFlightsAsync([FromBody] LoadCheapestFlightViewModel model)
        {
            if (model == null)
            {
                model = new LoadCheapestFlightViewModel();
                TryValidateModel(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get original local time.
            var originalLocalTime = _baseTimeService.UnixToDateTimeUtc(model.OriginalFlightSearchTime);

            // List of tasks that used for loading cheapeast flight sessions.
            var loadCheapestFlightPricesSessionsTasks = new List<Task<string>>();

            #region Load flight sessions

            for (var i = 0; i < 30; i++)
            {
                var dateFlight = originalLocalTime.AddDays(i);

                var loadCheapestFlightConditions = new CreateFlightSearchSessionViewModel();
                loadCheapestFlightConditions.OriginPlace = model.Departure;
                loadCheapestFlightConditions.DestinationPlace = model.Arrival;
                loadCheapestFlightConditions.OutboundDate = dateFlight.ToString("yyyy-MM-dd");

                //Get price
                var loadCheapestFlightPricesSessionsTask = _skyScannerService.CreateFlightSearchSessionAsync(loadCheapestFlightConditions);
                loadCheapestFlightPricesSessionsTasks.Add(loadCheapestFlightPricesSessionsTask);
            }

            // Wait for all sessions to be retrieved.
            var flightSearchSessions = await Task.WhenAll(loadCheapestFlightPricesSessionsTasks);
            flightSearchSessions =
                flightSearchSessions.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            #endregion

            #region Load cheapest flights

            var loadCheapestFlightTasks = new List<Task<ResultPricesViewModel>>();
            foreach (var flightSearchSession in flightSearchSessions)
            {
                var loadFlightInformationTask = _skyScannerService.LoadFightInformationAsync(flightSearchSession);
                loadCheapestFlightTasks.Add(loadFlightInformationTask);
            }

            var loadCheapestFlightResults = await Task.WhenAll(loadCheapestFlightTasks);

            #endregion

            // Initialize list of response.
            var cheapestFlights = new List<CheapestFlightViewModel>();
            
            foreach (var loadCheapestFlightResult in loadCheapestFlightResults)
            {
                var itineraries = loadCheapestFlightResult.Itineraries;
                if (itineraries == null || itineraries.Count < 1)
                    continue;

                var query = loadCheapestFlightResult.Query;
                if (query == null)
                    continue;

                var cheapestPrice = new CheapestFlightViewModel();
                cheapestPrice.Price = itineraries.Where(x => x.PricingOptions != null).SelectMany(x => x.PricingOptions)
                    .Select(x => x.Price).FirstOrDefault();
                cheapestPrice.OutboundTime =
                    _baseTimeService.DateTimeUtcToUnix(query.OutboundDate.DateTime.ToUniversalTime());

                cheapestFlights.Add(cheapestPrice);
            }

            return Ok(cheapestFlights);
        }
        
        
        #endregion
    }
}