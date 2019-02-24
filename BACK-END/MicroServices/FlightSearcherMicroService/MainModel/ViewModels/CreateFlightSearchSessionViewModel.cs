using System;
using System.ComponentModel.DataAnnotations;

namespace MainModel.ViewModels
{
    public class CreateFlightSearchSessionViewModel
    {
        #region Properties

        /// <summary>
        /// The market/country your user is in (see docs for list of markets)
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// The currency you want the prices in (3-letter currency code)
        /// </summary>
        [Required]
        public string Currency { get; set; }

        /// <summary>
        /// The locale you want the results in (ISO locale)
        /// </summary>
        [Required]
        public string Locale { get; set; }

        /// <summary>
        /// The origin place (see docs for places)
        /// </summary>
        [Required]
        public string OriginPlace { get; set; }

        /// <summary>
        /// The destination place (see docs for places)
        /// </summary>
        [Required]
        public string DestinationPlace { get; set; }

        /// <summary>
        /// The outbound date. Format “yyyy-mm-dd”.
        /// </summary>
        [Required]
        public string OutboundDate { get; set; }

        public int Adults { get; set; }

        /// <summary>
        /// The return date. Format “yyyy-mm-dd”. Use empty string for oneway trip.
        /// </summary>
        public string InboundDate { get; set; }

        /// <summary>
        /// The cabin class. Can be “economy”, “premiumeconomy”, “business”, “first”
        /// </summary>
        public string CabinClass { get; set; }

        /// <summary>
        /// Number of children (1-16 years). Can be between 0 and 8.
        /// </summary>
        public int Children { get; set; }

        /// <summary>
        /// Number of infants (under 12 months). Can be between 0 and 8
        /// </summary>
        public int Infants { get; set; }

        /// <summary>
        /// Only return results from those carriers. Comma-separated list of carrier ids
        /// </summary>
        public string IncludeCarriers { get; set; }

        /// <summary>
        /// Filter out results from those carriers. Comma-separated list of carrier ids
        /// </summary>
        public string ExcludeCarriers { get; set; }

        /// <summary>
        /// If set to true, prices will be obtained for the whole passenger group and if set to false it will be obtained for one adult. 
        /// By default it is set to false.
        /// </summary>
        public bool GroupPricing { get; set; }

        #endregion

        #region Constructor

        public CreateFlightSearchSessionViewModel()
        {
            Country = "US";
            Currency = "USD";
            Locale = "en-US";
            OriginPlace = "SFO-sky";
            DestinationPlace = "LHR-sky";
            OutboundDate = DateTime.Now.ToString("yyyy-MM-dd");
            CabinClass = "business";
            Adults = 1;
        }

        public CreateFlightSearchSessionViewModel(string country, string currency, string locale, string originPlace, string destinationPlace, string outboundDate, int adults)
        {
            Country = country;
            Currency = currency;
            Locale = locale;
            OriginPlace = originPlace;
            DestinationPlace = destinationPlace;
            OutboundDate = outboundDate;
            Adults = adults;
        }



        #endregion
    }
}