using System.Collections.Generic;
using System.Threading.Tasks;
using MainBusiness.Interfaces.Services;
using MainBusiness.Services.Pushers;
using ServiceShared.Models;

namespace MainBusiness.Services
{
    public class InternalMqService : IInternalMqService
    {
        #region Properties

        private readonly MessageBroadcasterPusherClient _messageBroadcasterPusherClient;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initialize mq service.
        /// </summary>
        public InternalMqService(MessageBroadcasterPusherClient messageBroadcasterPusherClient)
        {
            _messageBroadcasterPusherClient = messageBroadcasterPusherClient;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <returns></returns>
        public virtual async Task SendInternalMqAsync<T>(HashSet<int> recipientIds,
            HashSet<string> targetedGroups = null, string[] channelNames = null, string eventName = null,
            string message = null, T extraInfo = default(T),
            bool bIsExpressionSupressed = default(bool))
        {
            var model = new InternalMqMessage();
            model.RecipientIds = recipientIds;
            model.TargetedGroups = targetedGroups;
            model.EventName = eventName;
            model.ChannelNames = channelNames;
            model.EventName = eventName;
            model.Message = message;

            model.ExtraInfo = extraInfo;

            try
            {
                await _messageBroadcasterPusherClient
                    .TriggerAsync(channelNames, eventName, model);
            }
            catch
            {
                if (!bIsExpressionSupressed)
                    throw;
            }
        }

        #endregion
    }
}