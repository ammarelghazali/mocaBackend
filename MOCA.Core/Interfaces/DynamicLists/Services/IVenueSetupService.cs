using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.DynamicLists.Services
{
    public interface IVenueSetupService
    {
        Task<Response<long>> AddVenueSetup(VenueSetupModel request);
        Task<Response<List<VenueSetup>>> AddListOfVenueSetup(List<VenueSetupModel> request);
        Task<Response<bool>> UpdateVenueSetup(VenueSetupModel request);

        Task<PagedResponse<List<VenueSetupModel>>> GetAllVenueSetupPaginated(RequestParameter filter);

        Task<Response<List<VenueSetupModel>>> GetVenueSetupWithoutPagination();
        Task<Response<VenueSetupModel>> GetVenueSetupById(long Id);
        Task<Response<bool>> DeleteVenueSetup(long Id);
    }
}
