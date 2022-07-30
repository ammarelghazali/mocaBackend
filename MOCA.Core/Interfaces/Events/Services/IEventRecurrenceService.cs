using MOCA.Core.DTOs.Events.EventReccuranceDtos.Request;
using MOCA.Core.DTOs.Events.EventReccuranceDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventRecurrenceService
    {
        Task<PagedResponse<IReadOnlyList<get_AllEventReccurance_ViewModel>>>
                                                    GetAll(get_AllEventReccurance_Query request);
    }
}
