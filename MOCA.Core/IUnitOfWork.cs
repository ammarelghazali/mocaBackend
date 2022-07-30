﻿using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.LocationManagment.Repositories;

namespace MOCA.Core
{
    public interface IUnitOfWork : IDisposable
    {
        #region Moca Settings

        #endregion

        #region Location Managment

        IRepository<Country> CountryRepo { get; }
        ICountryRepository CountryRepoEF { get; }
        IRepository<City> CityRepo { get; }
        ICityRepository CityRepoEF { get; }

        #endregion

        void Save();
        Task<int> SaveAsync();
        DateTime? GetServerDate();
        DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone);

        DbContext contextForTransaction { get; }
    }
}
