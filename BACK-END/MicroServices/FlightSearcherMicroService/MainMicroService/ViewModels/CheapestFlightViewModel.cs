namespace MainMicroService.ViewModels
{
    public class CheapestFlightViewModel
    {
        #region Properties

        /// <summary>
        /// Outbound time.
        /// </summary>
        public double OutboundTime { get; set; }

        /// <summary>
        /// Flight price.
        /// </summary>
        public double Price { get; set; }

        #endregion

        #region Constructor

        public CheapestFlightViewModel()
        {
        }

        public CheapestFlightViewModel(double outboundTime, double price)
        {
            OutboundTime = outboundTime;
            Price = price;
        }

        #endregion
    }
}