﻿using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System.Data.Entity;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;
        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasAnyDistricts(long cityId)
        {
            if (cityId <= 0) return false;
            return await _context.Districts.AnyAsync(x => x.CityId.Equals(cityId) && x.IsDeleted == false);
        }

        public async Task<IReadOnlyList<City>> GetCitiesByCountryId(long countryId)
        {
            return await _context.Cities.Where(a => a.CountryId == countryId && a.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> DeleteCity(long Id)
        {
            var city = _context.Cities.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (city == null)
            {
                return false;
            }
            _context.Cities.Remove(city);
            return true;
        }
    }
}
