using Newtonsoft.Json;

namespace MainModel.Models
{
    public class SkyScannerValidationError
    {
        #region Properties

        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public string Message { get; set; }

        #endregion

        #region Constructor

        [JsonConstructor]
        public SkyScannerValidationError(string parameterName, string parameterValue, string message)
        {
            ParameterName = parameterName;
            ParameterValue = parameterName;
            Message = message;
        }

        #endregion
    }
}