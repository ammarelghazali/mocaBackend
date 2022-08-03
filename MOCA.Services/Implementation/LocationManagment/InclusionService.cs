using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Inclusion;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class InclusionService : IInclusionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public InclusionService(
            IAuthenticatedUserService authenticatedUserService, 
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }

        public async Task<Response<long>> AddInclusion(InclusionModel request)
        {
            var inclusion = _mapper.Map<Inclusion>(request);
            if (string.IsNullOrWhiteSpace(inclusion.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { inclusion.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (inclusion.CreatedAt == null || inclusion.CreatedAt == default)
            {
                inclusion.CreatedAt = _dateTimeService.NowUtc;
            }
            var entityInclusion = await _unitOfWork.InclusionRepo.GetByIdAsync(request.Id);
            if (entityInclusion == null)
            {
                throw new NotFoundException(nameof(Inclusion), request.Id);
            }

            _unitOfWork.InclusionRepo.Insert(inclusion);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Inclusion right now");
            }

            return new Response<long>(inclusion.Id, "Inclusion Added Successfully.");
        }

        public async Task<Response<bool>> UpdateInclusion(InclusionModel request)
        {
            var inclusion = _mapper.Map<Inclusion>(request);

            if (string.IsNullOrWhiteSpace(inclusion.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { inclusion.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (inclusion.LastModifiedAt == null)
            {
                inclusion.LastModifiedAt = DateTime.UtcNow;
            }

            var inclusionEntity = await _unitOfWork.InclusionRepo.GetByIdAsync(request.Id);
            if (inclusionEntity == null) { throw new NotFoundException(nameof(Inclusion), request.Id); }
            inclusion.CreatedBy = inclusionEntity.CreatedBy;
            inclusion.CreatedAt = inclusionEntity.CreatedAt;

            _unitOfWork.InclusionRepo.Update(inclusion);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Inclusion right now");
            }

            return new Response<bool>(true, "Inclusion Updated Successfully.");
        }

        public async Task<Response<InclusionModel>> GetInclusionByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (Id <= 0)
            {
                return new Response<InclusionModel>("ID must be greater than zero.");
            }
            var inclusion = await _unitOfWork.InclusionRepo.GetByIdAsync(Id);
            if (inclusion == null)
            {
                return new Response<InclusionModel>(null, "No Feature Found With This ID.");
            }
            return new Response<InclusionModel>(_mapper.Map<InclusionModel>(inclusion));
        }

        public async Task<PagedResponse<List<InclusionModel>>> GetAllInclusionsWithPagination(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            int pg_total = await _unitOfWork.InclusionRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.InclusionRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<InclusionModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<InclusionModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<InclusionModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<InclusionModel>>> GetAllInclusionsWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var data = _unitOfWork.InclusionRepo.GetAll();

            var Res = _mapper.Map<List<InclusionModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<InclusionModel>>(null);
            }
            return new Response<List<InclusionModel>>(Res);
        }

        public async Task<Response<bool>> DeleteInclusion(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var HasAnyRelatedEntities = await _unitOfWork.InclusionRepoEF.HasAnyRelatedEntities(Id);
            if (HasAnyRelatedEntities)
            {
                throw new EntityIsBusyException("Inclusion Is Busy and Can't be deleted.");
            }

            var feature = await _unitOfWork.InclusionRepoEF.DeleteInclusion(Id);
            if (feature == false)
                return new Response<bool>("Inclusion With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Inclusion right now");
            }
            return new Response<bool>(true, "Inclusion Deleted Successfully.");
        }
    }
}