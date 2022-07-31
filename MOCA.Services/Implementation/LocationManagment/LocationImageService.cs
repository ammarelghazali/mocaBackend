using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationImageService : ILocationImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        public LocationImageService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<bool>> AddLocationImages(List<LocationImageModel> request)
        {
            var locationImages = _mapper.Map<List<LocationImage>>(request);
            for (int i = 0; i < locationImages.Count; i++)
            {
                locationImages[i].CreatedBy = "System";
                if (locationImages[i].CreatedAt == null || locationImages[i].CreatedAt == default)
                {
                    locationImages[i].CreatedAt = _dateTimeService.NowUtc;
                }
            }
            /*if (string.IsNullOrWhiteSpace(city.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { city.CreatedBy = authenticatedUserService.UserId; }
            }*/

            _unitOfWork.LocationImageRepo.InsertRang(locationImages);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Add LocationImage right now");
            }*/

            return new Response<bool>(true, "Location Image Added Successfully.");
        }

        public async Task<Response<bool>> DeleteLocationImages(long LocationID)
        {
            /*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
               {
                   throw new UnauthorizedAccessException("User is not authorized");
               }
           */

            var LocationImage = await _unitOfWork.LocationImageRepoEF.DeleteAllLocationImageByLocationID(LocationID);
            if (LocationImage == false)
                return new Response<bool>("Location Image With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location Image right now");
            }
            return new Response<bool>(true, "Location Image Deleted Successfully.");
        }

        public async Task<Response<List<LocationImageModel>>> GetLocationImagesByLocationID(long LocationID)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
             */
            if (LocationID <= 0)
            {
                return new Response<List<LocationImageModel>>("ID must be greater than zero.");
            }
            var LocationImage = await _unitOfWork.LocationImageRepoEF.GetAllLocationImageByLocationID(LocationID);
            if (LocationImage == null)
            {
                return new Response<List<LocationImageModel>>(null, "No Location Image Found With This ID.");
            }
            return new Response<List<LocationImageModel>>(_mapper.Map<List<LocationImageModel>>(LocationImage));
        }
    }
}
