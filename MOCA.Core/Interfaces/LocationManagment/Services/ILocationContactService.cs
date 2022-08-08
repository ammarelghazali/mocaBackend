using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationContactService
    {
        Task<Response<bool>> AddLocationContacts(List<LocationContactModel> request);
        Task<Response<bool>> DeleteLocationContacts(long LocationID);
        Task<Response<List<LocationContactModel>>> GetLocationContactsByLocationID(long LocationID);
    }
}
