using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainBusiness.Interfaces.Services
{
    public interface IInternalMqService
    {
        #region Internal mq service

        /// <summary>
        ///     Add notification message to system.
        /// </summary>
        /// <returns></returns>
        Task SendInternalMqAsync<T>(HashSet<int> recipientIds,
            HashSet<string> targetedGroups = null, string[] channelNames = null, string eventName = null,
            string message = null, T extraInfo = default(T),
            bool bIsExpressionSupressed = default(bool));

        #endregion
    }
}