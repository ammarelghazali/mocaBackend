using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Industry;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class IndustryService : IIndustryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public IndustryService(
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

        public async Task<Response<long>> AddIndustry(IndustryModel request)
        {
            var industry = _mapper.Map<Industry>(request);
            if (string.IsNullOrWhiteSpace(industry.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { industry.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (industry.CreatedAt == null || industry.CreatedAt == default)
            {
                industry.CreatedAt = _dateTimeService.NowUtc;
            }
            var entityIndustry = await _unitOfWork.IndustryRepo.GetByIdAsync(request.Id);
            if (entityIndustry == null)
            {
                throw new NotFoundException(nameof(Industry), request.Id);
            }

            _unitOfWork.IndustryRepo.Insert(industry);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Industry right now");
            }

            return new Response<long>(industry.Id, "Industry Added Successfully.");
        }

        public async Task<Response<bool>> UpdateIndustry(IndustryModel request)
        {
            var industry = _mapper.Map<Industry>(request);

            if (string.IsNullOrWhiteSpace(industry.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { industry.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (industry.LastModifiedAt == null)
            {
                industry.LastModifiedAt = DateTime.UtcNow;
            }

            var industryEntity = await _unitOfWork.IndustryRepo.GetByIdAsync(request.Id);
            if (industryEntity == null) { throw new NotFoundException(nameof(Industry), request.Id); }
            industry.CreatedBy = industryEntity.CreatedBy;
            industry.CreatedAt = industryEntity.CreatedAt;

            _unitOfWork.IndustryRepo.Update(industry);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Industry right now");
            }

            return new Response<bool>(true, "Industry Updated Successfully.");
        }

        public async Task<Response<IndustryModel>> GetIndustryByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (Id <= 0)
            {
                return new Response<IndustryModel>("ID must be greater than zero.");
            }
            var industry = await _unitOfWork.IndustryRepo.GetByIdAsync(Id);
            if (industry == null)
            {
                return new Response<IndustryModel>(null, "No Industry Found With This ID.");
            }
            return new Response<IndustryModel>(_mapper.Map<IndustryModel>(industry));
        }

        public async Task<PagedResponse<List<IndustryModel>>> GetAllIndustriesWithPagination(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            int pg_total = await _unitOfWork.IndustryRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.IndustryRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<IndustryModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<IndustryModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<IndustryModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<IndustryModel>>> GetAllIndustriesWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var data = _unitOfWork.IndustryRepo.GetAll();

            var Res = _mapper.Map<List<IndustryModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<IndustryModel>>(null, "Not Data Found");
            }
            return new Response<List<IndustryModel>>(Res);
        }

        public async Task<Response<bool>> DeleteIndustry(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var HasAnyRelatedEntities = await _unitOfWork.IndustryRepoEF.HasAnyRelatedEntities(Id);
            if (HasAnyRelatedEntities)
            {
                throw new EntityIsBusyException("Industry Is Busy and Can't be deleted.");
            }

            var industry = await _unitOfWork.IndustryRepoEF.DeleteIndustry(Id);
            if (industry == false)
                return new Response<bool>("Industry With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Industry right now");
            }
            return new Response<bool>(true, "Industry Deleted Successfully.");
        }
    }
}
