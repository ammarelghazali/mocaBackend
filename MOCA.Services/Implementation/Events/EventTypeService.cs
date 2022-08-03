using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventTypeDtos.Requset;
using MOCA.Core.DTOs.Events.EventTypeDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Services;

namespace MOCA.Services.Implementation.Events
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EventTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedResponse<IReadOnlyList<AllEventTypesDto>>> GetAllEventTypes(GetAllEventTypeDto filter)
        {
            var allEventTypes = await _unitOfWork.EventTypeRepo.GetAllWithFilterAsync(x => x.IsDeleted != true);
            var dataViewModel = _mapper.Map<List<AllEventTypesDto>>(allEventTypes);
            return new PagedResponse<IReadOnlyList<AllEventTypesDto>>(dataViewModel,
                filter.pageNumber, filter.pageSize, allEventTypes.Count());
        }
    };
}
