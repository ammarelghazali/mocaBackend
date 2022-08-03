using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventAttendanceDtos.Request;
using MOCA.Core.DTOs.Events.EventAttendanceDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Services;

namespace MOCA.Services.Implementation.Events
{
    public class EventAttendanceService : IEventAttendanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EventAttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedResponse<IReadOnlyList<GetAllEventAttendanceViewModel>>> GetAll(GetAllEventAttendanceDto getAllEventAttendanceDto)
        {
            var allEventAttendance = await _unitOfWork.EventAttendanceRepo.GetAllWithFilterAsync(x => x.IsDeleted != true);
            var dataViewModel = _mapper.Map<List<GetAllEventAttendanceViewModel>>(allEventAttendance);
            return new PagedResponse<IReadOnlyList<GetAllEventAttendanceViewModel>>(dataViewModel,
                getAllEventAttendanceDto.pageNumber, getAllEventAttendanceDto.pageSize, allEventAttendance.Count());
        }

    }
}
