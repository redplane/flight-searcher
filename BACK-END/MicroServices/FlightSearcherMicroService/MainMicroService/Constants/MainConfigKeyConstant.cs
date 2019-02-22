namespace MainMicroService.Constants
{
    public class MainConfigKeyConstant
    {
        #region Properties

        /// <summary>
        ///     Configuration key of jwt.
        /// </summary>
        public const string AppJwt = "appJwt";

        /// <summary>
        ///     Configuration of firebase connection.
        /// </summary>
        public const string AppFirebase = "appFirebase";

        /// <summary>
        ///     Sendgrid configuration.
        /// </summary>
        public const string AppSendGrid = "appSendGrid";

        public const string SqliteConnectionString = "sqliteConnectionString";

        public const string SqlConnectionString = "sqlConnectionString";

        /// <summary>
        ///     Redis clients.
        /// </summary>
        public const string CacheClients = "cacheClients";

        /// <summary>
        ///     Pusher real-time channels.
        /// </summary>
        public const string PusherRealTimeChannels = "pusherRealTimeChannels";

        #endregion
    }
}