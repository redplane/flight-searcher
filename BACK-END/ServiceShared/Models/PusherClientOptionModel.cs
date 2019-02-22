namespace ServiceShared.Models
{
    public class PusherClientOptionModel
    {
        #region Properties

        public string AppId { get; set; }

        public string Key { get; set; }

        public string Secret { get; set; }

        public string Cluster { get; set; }

        public bool Encrypted { get; set; }

        #endregion
    }
}