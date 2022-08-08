using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IEventSpaceBookingRepository : IGenericRepository<EventSpaceBooking>
    {
        Task<int> BookEventSpace(EventSpaceBooking eventSpaceBooking);
        Task<int> UpdateEventSpaceBooking(EventSpaceBooking eventSpaceBooking);
        Task<int> UpdateEventSpaceBookingWebsite(EventSpaceBooking eventSpaceBooking);
        Task<int> CountGetBookedEventSpaceById(long locationTypeId);
        Task<List<EventSpaceBooking>> GetAllBookedEventSpaced();
        Task<List<EventSpaceBooking>> GetAllBookedEventSpaceByLocationId(long locationId);
        Task<List<EventSpaceBooking>> GetAllBookedEventSpaceByLocationTypeId(long locationTypeId);
        Task<List<long>> GetAllDistinctLocation();
        Task<List<long>> GetAllDistinctRequester(long? locationId);
        Task<List<long>> GetAllDistinctIndusty(long? locationId);
        Task<List<long>> GetAllDistinctCategory(long? locationId);
        Task<List<long>> GetAllDistinctReccurance(long? locationId);
        Task<List<long>> GetAllDistinctType(long? locationId);
        Task<List<long>> GetAllDistinctAttendance(long? locationId);
        Task<List<long>> GetAllDistinctInitiated();
        Task<EventSpaceBooking> CheckEmailAndMobileExist(string email, string mobile);
        Task<bool> DeleteEventOpportunitiyByID(long id);
        Task<bool> CheckEventOpportunitiyExistenceByID(long id);
        Task<bool> UpdateEventOpportunitiyStageReportId(long id, long opportunityStageId);
        Task<List<EventSpaceBooking>> GetBookedEventSpaceByIdAsync(long locationTypeId, int pageNumber, int pageSize);

    }
}
