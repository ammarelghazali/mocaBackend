using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs;
using MOCA.Core.DTOs.LocationManagment.Amenity;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public AmenityService(IAuthenticatedUserService authenticatedUserService, IMapper mapper, IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }
        public async Task<Response<long>> AddAmenity(AmenityModel request)
        {
            var amenity = _mapper.Map<Amenity>(request);

            if (string.IsNullOrWhiteSpace(amenity.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { amenity.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (amenity.CreatedAt == null || amenity.CreatedAt == default)
            {
                amenity.CreatedAt = _dateTimeService.NowUtc;
            }
            var amenityEntity = await _unitOfWork.AmenityRepoEF.IsUniqueNameAsync(request.Name);

            if (!amenityEntity)
            {
                return new Response<long>("This Amenity is already exist");
            }

            _unitOfWork.AmenityRepo.Insert(amenity);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Amenity right now");
            }

            return new Response<long>(amenity.Id, "Amenity Added Successfully.");
        }

        public async Task<Response<List<AmenityModel>>> AddListOfAmenity(List<AmenityModel> request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeleteAmenity(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<AmenityModel>>> GetAllAmenityPaginated(RequestParameter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<AmenityModel>> GetAmenityById(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<AmenityModel>>> GetAmenityWithoutPagination()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> UpdateAmenity(AmenityModel request)
        {
            throw new NotImplementedException();
        }
    }
}
