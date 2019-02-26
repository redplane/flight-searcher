using System.ComponentModel.DataAnnotations;

namespace MainMicroService.ViewModels
{
    public class LoadCheapestFlightViewModel
    {
        #region Properties

        /// <summary>
        /// Departure place.
        /// </summary>
        [Required]
        public string Departure { get; set; }

        /// <summary>
        /// Arrival place.
        /// </summary>
        [Required]
        public string Arrival { get; set; }

        /// <summary>
        /// Time when flight search is started.
        /// </summary>
        public double OriginalFlightSearchTime { get; set; }

        #endregion
    }
}