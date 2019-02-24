namespace MainModel.Models
{
    public class SkyScannerFlightSearchConfiguration
    {
        #region Properties

        /// <summary>
        /// Api base url.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Api key that is used for connecting to skyscanner api end-point.
        /// </summary>
        public string ApiKey { get; set; }

        #endregion

        #region Constructor

        public SkyScannerFlightSearchConfiguration()
        {
            BaseUrl = "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com";
        }

        #endregion
    }
}