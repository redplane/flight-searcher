using System;
using System.Collections.Generic;
using MainModel.Models;

namespace MainModel.Exceptions
{
    public class SkyScannerBadRequestException: Exception
    {
        #region Properties
        
        /// <summary>
        /// Validation errors that skyscanner api returns.
        /// </summary>
        public List<SkyScannerValidationError> ValidationErrors { get; set; }

        #endregion

        #region Constructor

        public SkyScannerBadRequestException()
        {
            ValidationErrors = new List<SkyScannerValidationError>();
        }

        public SkyScannerBadRequestException(List<SkyScannerValidationError> validationErrors) : this()
        {
            ValidationErrors = validationErrors;
        }

        #endregion
        
    }
}