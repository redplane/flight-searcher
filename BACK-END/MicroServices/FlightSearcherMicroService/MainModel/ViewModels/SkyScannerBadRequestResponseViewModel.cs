using System.Collections.Generic;
using MainModel.Models;

namespace MainModel.ViewModels
{
    public class SkyScannerBadRequestResponseViewModel
    {
        #region Properties

        /// <summary>
        /// List of validation errors.
        /// </summary>
        public List<SkyScannerValidationError> ValidationErrors { get; set; }

        #endregion

        #region Constructor

        public SkyScannerBadRequestResponseViewModel()
        {
            ValidationErrors = new List<SkyScannerValidationError>();
        }

        #endregion
    }
}