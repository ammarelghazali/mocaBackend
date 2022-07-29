using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core
{
    public interface IUnitOfWork : IDisposable
    {
        #region Moca Settings

        #endregion

        #region Location Managment

        IRepository<Country> CountryRepo { get; }


        #endregion

        void Save();
        Task<int> SaveAsync();
        DateTime? GetServerDate();
        DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone);

        DbContext contextForTransaction { get; }
    }
}
