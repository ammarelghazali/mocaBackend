using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckLocationNameIsUinque(string LocationName)
        {
            var locationName = _context.Locations.Where(x => x.Name == LocationName && x.IsDeleted != true).FirstOrDefault();
            if (locationName == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLocation(long Id)
        {
            var location = _context.Locations.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (location == null)
            {
                return false;
            }
            _context.Locations.Remove(location);
            return true;
        }

        public async Task<List<long>> GetAllDistinictDistrict()
        {
            var locationDistricts = _context.Locations.Where(x => x.IsDeleted != true).Select(x => x.DistrictId).Distinct().ToList();
            if (locationDistricts == null)
            {
                return new List<long>(null);
            }
            return new List<long>(locationDistricts);
        }
        
        public async Task<List<DropdownViewModel>> GetAllDistinictLocation()
        {
            var location = _context.Locations.Where(x => x.IsDeleted != true).Select(x => new DropdownViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            if (location == null)
            {
                return new List<DropdownViewModel>(null);
            }
            return new List<DropdownViewModel>(location);
        }

        public async Task<List<LocationGetAllModel>> GetAllUnpublishedLocation()
        {
            var location = _context.Locations.Where(x => x.IsDeleted != true && x.IsPublish == false).Select(x => new LocationGetAllModel
            {
                Id = x.Id,
                Name = x.Name,
                ContractLength = x.ContractLength,
                ContractStartDate = x.ContractStartDate,
                GrossArea = x.GrossArea,
                NetArea = x.NetArea,
                IsPublish = x.IsPublish,
                LocationType = new DropdownViewModel
                {
                    Id = x.LocationTypeId
                },
                District = new DropdownViewModel
                {
                    Id = x.DistrictId
                },
                City = new DropdownViewModel()
            }).AsNoTracking().ToList();

            if (location == null)
            {
                return new List<LocationGetAllModel>(null);
            }
            return new List<LocationGetAllModel>(location);
        }

        public async Task<List<LocationGetAllModel>> GetAllPublishedAndUnpublishedLocation()
        {
            var location = _context.Locations.Where(x => x.IsDeleted != true).Select(x => new LocationGetAllModel
            {
                Id = x.Id,
                Name = x.Name,
                ContractLength = x.ContractLength,
                ContractStartDate = x.ContractStartDate,
                GrossArea = x.GrossArea,
                NetArea = x.NetArea,
                IsPublish = x.IsPublish,
                LocationType = new DropdownViewModel
                {
                    Id = x.LocationTypeId
                },
                District = new DropdownViewModel
                {
                    Id = x.DistrictId
                },
                City = new DropdownViewModel()
            }).AsNoTracking().ToList();

            if (location == null)
            {
                return new List<LocationGetAllModel>(null);
            }
            return new List<LocationGetAllModel>(location);
        }
    }
}
