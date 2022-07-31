using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IOpportunityStageRepository : IGenericRepository<OpportunityStage>
    {
        Task<List<OpportunitySatgeModel_ViewModel>> GetDefaultStage();
        Task<OpportunitySatgeModel_ViewModel> GetCurrentStage(long OpportunityStage_ID);
    }
}
