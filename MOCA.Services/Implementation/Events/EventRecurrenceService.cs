using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventReccuranceDtos.Request;
using MOCA.Core.DTOs.Events.EventReccuranceDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Services;

namespace MOCA.Services.Implementation.Events
{
    public class EventRecurrenceService : IEventRecurrenceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EventRecurrenceService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedResponse<IReadOnlyList<GetAllEventReccuranceViewModel>>> GetAll(GetAllEventReccuranceQuery request)
        {
            var allEventReccurance = await _unitOfWork.EventReccuranceRepo.GetAllWithFilterAsync(x => x.IsDeleted == false);
            var dataViewModel = _mapper.Map<List<GetAllEventReccuranceViewModel>>(allEventReccurance);
            return new PagedResponse<IReadOnlyList<GetAllEventReccuranceViewModel>>(dataViewModel,
                request.pageNumber, request.pageSize, allEventReccurance.Count());
        }
    }
}
