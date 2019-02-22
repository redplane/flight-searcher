using MainDb.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceShared.Services;

namespace MainDb.Services
{
    public class AppUnitOfWork : BaseUnitOfWork, IAppUnitOfWork
    {
        #region Constructors

        /// <summary>
        ///     Initiate unit of work with database context provided by Entity Framework.
        /// </summary>
        public AppUnitOfWork(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion

        #region Properties

        #endregion
    }
}