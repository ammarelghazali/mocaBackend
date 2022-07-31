using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventRequesterDtos.Request;
using MOCA.Core.DTOs.Events.EventRequesterDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Services;

namespace MOCA.Services.Implementation.Events
{
    public class EventRequesterService : IEventRequesterService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EventRequesterService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedResponse<IReadOnlyList<GetAllEventRequesterResponseDto>>> GetAllEventRequester(GetAllEventRequesterDto filter)
        {
            var allEventRequester = await _unitOfWork.EventRequesterRepo.GetAllWithFilterAsync(x => x.IsDeleted == false);
            var dataViewModel = _mapper.Map<List<GetAllEventRequesterResponseDto>>(allEventRequester);
            return new PagedResponse<IReadOnlyList<GetAllEventRequesterResponseDto>>(dataViewModel,
                filter.pageNumber, filter.pageSize, allEventRequester.Count());
        }
    }
}
