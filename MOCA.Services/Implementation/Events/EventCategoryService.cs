using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventCategoryDtos.Request;
using MOCA.Core.DTOs.Events.EventCategoryDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Services;

namespace MOCA.Services.Implementation.Events
{
    public class EventCategoryService : IEventCategoryService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EventCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedResponse<IReadOnlyList<get_AllEventCategory_ViewModel>>> GetAll(get_AllEventCategory_Query request)
        {
            var allEventCategory = await _unitOfWork.EventCategoryRepo.GetAllWithFilterAsync(x => x.IsDeleted == false);
            var dataViewModel = mapper.Map<List<get_AllEventCategory_ViewModel>>(allEventCategory);
            return new PagedResponse<IReadOnlyList<get_AllEventCategory_ViewModel>>(dataViewModel,
                request.pageNumber, request.pageSize, allEventCategory.Count());
        }
    }
}
