namespace ServiceShared.Constants
{
    public class AppConfigKeyConstants
    {
        #region Properties

        /// <summary>
        ///     Configuration key of jwt.
        /// </summary>
        public const string AppJwt = "appJwt";

        /// <summary>
        ///     Configuration of Google OAuth.
        /// </summary>
        public const string GoogleCredential = "googleCredential";

        /// <summary>
        ///     Configuration of facebook credential.
        /// </summary>
        public const string FacebookCredential = "facebookCredential";

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

        #endregion
    }
}