using ServiceShared.Interfaces;
using ServiceStack.OrmLite;

namespace ServiceShared.Services
{
    public class AccessTokenDbConnection : OrmLiteConnection, IAccessTokenDbConnection
    {
        public AccessTokenDbConnection(OrmLiteConnectionFactory factory) : base(factory)
        {
        }
    }
}