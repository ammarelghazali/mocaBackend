using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.DynamicLists
{
    public class VenueSetupService : IVenueSetupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public VenueSetupService(IAuthenticatedUserService authenticatedUserService, IMapper mapper, IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }
        public async Task<Response<List<VenueSetup>>> AddListOfVenueSetup(List<VenueSetupModel> request)
        {
            var setup = _mapper.Map<List<VenueSetup>>(request);

            foreach (var item in setup)
            {
                if (string.IsNullOrWhiteSpace(item.CreatedBy))
                {
                    if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                    {
                        throw new UnauthorizedAccessException("User is not authorized");
                    }
                    else
                    { item.CreatedBy = _authenticatedUserService.UserId; }
                }
                if (item.CreatedAt == null || item.CreatedAt == default)
                {
                    item.CreatedAt = _dateTimeService.NowUtc;
                }
            }
            foreach (var r in request)
            {
                var setupEntity = await _unitOfWork.VenueSetupRepoEF.IsUniqueNameAsync(r.Name.ToString());
                if (!setupEntity)
                {
                    return new Response<List<VenueSetup>>("This Venue Setup is already exist");
                }
            }
            _unitOfWork.VenueSetupRepo.InsertRang(setup);

            if (await _unitOfWork.SaveAsync() < 1) { return new Response<List<VenueSetup>>("Cannot Add Venue Setup right now"); }

            return new Response<List<VenueSetup>>(setup, "Venue Setup Added Successfully");
        }

        public async Task<Response<long>> AddVenueSetup(VenueSetupModel request)
        {
            var setup = _mapper.Map<VenueSetup>(request);

            if (string.IsNullOrWhiteSpace(setup.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { setup.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (setup.CreatedAt == null || setup.CreatedAt == default)
            {
                setup.CreatedAt = _dateTimeService.NowUtc;
            }
            var setupEntity = await _unitOfWork.VenueSetupRepoEF.IsUniqueNameAsync(request.Name);

            if (!setupEntity)
            {
                return new Response<long>("This Venue Setup is already exist");
            }

            _unitOfWork.VenueSetupRepo.Insert(setup);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Venue Setup right now");
            }

            return new Response<long>(setup.Id, "Venue Setup Added Successfully.");
        }

        public async Task<Response<bool>> DeleteVenueSetup(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var setupEntity = await _unitOfWork.VenueSetupRepo.GetByIdAsync(Id);

            if (setupEntity == null)
            {
                // throw new NotFoundException(nameof(WorkSpaceCategory), Id);
                return new Response<bool>("This Venue Setup is not exist");
            }

            await _unitOfWork.VenueSetupRepoEF.DeleteVenueSetup(Id);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Venue Setup right now");
            }

            return new Response<bool>(true, "Venue Setup Deleted Successfully.");
        }

        public async Task<PagedResponse<List<VenueSetupModel>>> GetAllVenueSetupPaginated(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            int pg_total = await _unitOfWork.VenueSetupRepo.GetCountAsync(x => x.IsDeleted == false);

            var data = _unitOfWork.VenueSetupRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<VenueSetupModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<VenueSetupModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<VenueSetupModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<VenueSetupModel>> GetVenueSetupById(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<VenueSetupModel>("ID must be greater than zero.");
            }
            var setup = await _unitOfWork.VenueSetupRepo.GetByIdAsync(Id);
            if (setup == null)
            {
                return new Response<VenueSetupModel>("No Venue Setup Found With This ID.");
            }
            var res = _mapper.Map<VenueSetupModel>(setup);
            return new Response<VenueSetupModel>(res);
        }

        public async Task<Response<List<VenueSetupModel>>> GetVenueSetupWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.VenueSetupRepo.GetAll().ToList();
            var Res = _mapper.Map<List<VenueSetupModel>>(data);

            if (Res.Count == 0)
            {
                return new Response<List<VenueSetupModel>>(null);
            }
            return new Response<List<VenueSetupModel>>(Res);

        }

        public async Task<Response<bool>> UpdateVenueSetup(VenueSetupModel request)
        {
            var setup = _mapper.Map<VenueSetup>(request);

            if (string.IsNullOrWhiteSpace(setup.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { setup.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (setup.LastModifiedAt == null)
            {
                setup.LastModifiedAt = DateTime.UtcNow;
            }

            var setupEntity = await _unitOfWork.VenueSetupRepo.GetByIdAsync(request.Id);


            if (setupEntity == null) { return new Response<bool>(false, "This Venue Setup is exits before "); }

            setup.CreatedBy = setupEntity.CreatedBy;
            setup.CreatedAt = setupEntity.CreatedAt;

            _unitOfWork.VenueSetupRepo.Update(setup);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Venue Setup right now");
            }

            return new Response<bool>(true, " Venue Setup Updated Successfully.");
        }
    }
}
