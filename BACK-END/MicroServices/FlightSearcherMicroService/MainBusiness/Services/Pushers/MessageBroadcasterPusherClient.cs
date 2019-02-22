using PusherServer;

namespace MainBusiness.Services.Pushers
{
    public class MessageBroadcasterPusherClient : Pusher
    {
        #region Constructor

        /// <summary>
        ///     Initialize message broadcast pusher client.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="options"></param>
        public MessageBroadcasterPusherClient(string appId, string appKey, string appSecret,
            IPusherOptions options = null) : base(appId, appKey, appSecret, options)
        {
        }

        #endregion
    }
}