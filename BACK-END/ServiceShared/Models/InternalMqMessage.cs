using System.Collections.Generic;

namespace ServiceShared.Models
{
    public class InternalMqMessage
    {
        #region Properties

        /// <summary>
        ///     Recipients who should receive notification message.
        /// </summary>
        public HashSet<int> RecipientIds { get; set; }

        /// <summary>
        ///     Groups which should receive notification message.
        /// </summary>
        public HashSet<string> TargetedGroups { get; set; }

        /// <summary>
        ///     Mq realtime event name.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        ///     Real-time message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Extra information.
        /// </summary>
        public dynamic ExtraInfo { get; set; }

        /// <summary>
        ///     Mq channel names.
        /// </summary>
        public string[] ChannelNames { get; set; }

        #endregion

        #region Constructor

        #endregion
    }
}