using System.Threading;
using System.Threading.Tasks;
using MainModel.ViewModels;

namespace MainBusiness.Services.Interfaces
{
    public interface ISkyScannerService
    {
        #region Methods

        /// <summary>
        ///     Create a flight search session. A successful response contains no content.
        ///     The URL to poll the results is provided in the Location header of the response.
        /// </summary>
        /// <returns></returns>
        Task<string> CreateFlightSearchSessionAsync(CreateFlightSearchSessionViewModel model, CancellationToken cancellationToken = default(CancellationToken));

        Task<ResultPricesViewModel> LoadFightInformationAsync(string session,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}