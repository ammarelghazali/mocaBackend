﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MOCA.Core;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using MOCA.Presistence.Repositories.LocationManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public ApplicationDbContext _context { get; }
        public IConfiguration _configuration { get; }
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDateTimeService _dateTimeService;

        DbContext IUnitOfWork.contextForTransaction
        {
            get
            {
                return _context;
            }
        }

        public UnitOfWork(ApplicationDbContext context,
            IConfiguration configuration,
            IAuthenticatedUserService authenticatedUser,
            IDateTimeService dateTimeService)
        {
            _context = context;
            _configuration = configuration;
            _authenticatedUser = authenticatedUser;
            _dateTimeService = dateTimeService;
        }

        #region Moca Settings

        #endregion

        #region Location Managment

        private IRepository<Country> _countryRepo;
        public IRepository<Country> CountryRepo
        {
            get
            {
                return _countryRepo = _countryRepo ?? new Repository<Country>(_context);
            }
        }

        private ICountryRepository _countryRepoEF;
        public ICountryRepository CountryRepoEF
        {
            get
            {
                return _countryRepoEF = _countryRepoEF ?? new CountryRepository(_context);
            }
        }

        private IRepository<City> _cityRepo;
        public IRepository<City> CityRepo
        {
            get
            {
                return _cityRepo = _cityRepo ?? new Repository<City>(_context);
            }
        }

        private ICityRepository _cityRepoEF;
        public ICityRepository CityRepoEF
        {
            get
            {
                return _cityRepoEF = _cityRepoEF ?? new CityRepository(_context);
            }
        }

        private IRepository<District> _districtRepo;
        public IRepository<District> DistrictRepo
        {
            get
            {
                return _districtRepo = _districtRepo ?? new Repository<District>(_context);
            }
        }

        private IDistrictRepository _districtRepoEF;
        public IDistrictRepository DistrictRepoEF
        {
            get
            {
                return _districtRepoEF = _districtRepoEF ?? new DistrictRepository(_context);
            }
        }

        IRepository<Currency> _currencyRepo;
        public IRepository<Currency> CurrencyRepo
        {
            get
            {
                return _currencyRepo = _currencyRepo ?? new Repository<Currency>(_context);
            }
        }

        ICurrencyRepository _currencyRepoEF;
        public ICurrencyRepository CurrencyRepoEF
        {
            get
            {
                return _currencyRepoEF = _currencyRepoEF ?? new CurrencyRepository(_context);
            }
        }

        #endregion

        public DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone)
        {
            #region convert american time to utc time
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime UTCdateTime = TimeZoneInfo.ConvertTimeToUtc(dateInEasternTimeZone, easternZone);
            #endregion

            #region convert utc time to egyptian time
            TimeZoneInfo egyZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            DateTime EgyptdateTime = TimeZoneInfo.ConvertTimeFromUtc(UTCdateTime, egyZone);
            #endregion

            return EgyptdateTime;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DateTime? GetServerDate()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
