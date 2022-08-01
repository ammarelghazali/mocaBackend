using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.FavouriteLocation;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class FavouriteLocationService : IFavouriteLocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        public FavouriteLocationService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<long>> AddFavouriteLocation(FavouriteLocationModel request)
        {
            var FavouriteLocation = _mapper.Map<FavouriteLocation>(request);
            FavouriteLocation.CreatedBy = "System";
            /*if (string.IsNullOrWhiteSpace(city.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { city.CreatedBy = authenticatedUserService.UserId; }
            }*/
            if (FavouriteLocation.CreatedAt == null || FavouriteLocation.CreatedAt == default)
            {
                FavouriteLocation.CreatedAt = _dateTimeService.NowUtc;
            }

            _unitOfWork.FavouriteLocationRepo.Insert(FavouriteLocation);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add FavouriteLocation right now");
            }*/

            return new Response<long>(FavouriteLocation.Id, "FavouriteLocation Added Successfully.");
        }

        public async Task<Response<bool>> DeleteFavouriteLocation(long LocationId, long BasicUserID)
        {
            /*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
               {
                   throw new UnauthorizedAccessException("User is not authorized");
               }
           */

            var favouriteLocation = await _unitOfWork.FavouriteLocationRepoEF.DeleteFavouriteLocation(LocationId, BasicUserID);

            if (favouriteLocation == false)
                return new Response<bool>("Favourite Location With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Favourite Location right now");
            }
            return new Response<bool>(true, "Favourite Location Deleted Successfully.");
        }
    }
}
