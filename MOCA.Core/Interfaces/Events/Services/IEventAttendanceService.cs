using MOCA.Core.DTOs.Events.EventAttendanceDtos.Request;
using MOCA.Core.DTOs.Events.EventAttendanceDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventAttendanceService
    {
        Task<PagedResponse<IReadOnlyList<get_AllEventAttendance_ViewModel>>> GetAll(GetAllEventAttendanceDto request);
    }
}
