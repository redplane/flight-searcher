using System;
using ServiceStack.DataAnnotations;

namespace ServiceShared.Models.CachedEntities
{
    public class AccessToken
    {
        #region Properties

        [AutoId]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Hash { get; set; }

        public string Email { get; set; }

        public double IssuedTime { get; set; }

        public double? ExpiredTime { get; set; }

        public int LifeTime { get; set; }

        public string User { get; set; }

        #endregion
    }
}