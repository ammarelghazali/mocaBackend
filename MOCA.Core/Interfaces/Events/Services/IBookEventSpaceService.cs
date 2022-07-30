using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request;
using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IBookEventSpaceService
    {
        Task<Response<DropDownsResponseDto>> GetAllDataForDropDowns(GetAllBookedEventSpaceDropDownsDto request);

        Task<Response<long>> EventSpaceBooking(BooEventSpaceDto request);

        Task<PagedResponse<IReadOnlyList<GetAllBookedEventSpaceResponseDto>>> GetAllBookedEventSpaceTypeAsync(GetAllBookedEventSpaceByType_Query request);
    }
}
