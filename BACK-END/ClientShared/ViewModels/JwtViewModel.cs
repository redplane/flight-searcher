namespace ClientShared.ViewModels
{
    public class JwtViewModel
    {
        #region Properties

        /// <summary>
        ///     Access code which is used for accessing into system.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Life-time of
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        ///     When token should be expired.
        /// </summary>
        public double? ExpiredTime { get; set; }

        public double IssuedTime { get; set; }

        #endregion
    }
}