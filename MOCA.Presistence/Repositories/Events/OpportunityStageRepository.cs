using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class OpportunityStageRepository : GenericRepository<OpportunityStage>, IOpportunityStageRepository
    {
        private readonly ApplicationDbContext _context;
        public OpportunityStageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<OpportunitySatgeModelViewModel>> GetDefaultStage()
        {
            /*var OpportunityStages = _context.OpportunityStages.Where(x => x.Id >= 1 && x.Id <= 7).ToList();
            List<OpportunitySatgeModel_ViewModel> data = new List<OpportunitySatgeModel_ViewModel>();
            foreach (var item in OpportunityStages)
            {
                data.Add(new OpportunitySatgeModel_ViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsSelected = false
                });
            }
            return data;*/
            return _context.OpportunityStages.Where(x => x.Id >= 1 && x.Id <= 7 && x.IsDeleted != true)
                                              .Select(c => new OpportunitySatgeModelViewModel
                                              {
                                                  Id = c.Id,
                                                  Name = c.Name,
                                                  IsSelected = false
                                              }).ToList();
        }

        public async Task<OpportunitySatgeModelViewModel> GetCurrentStage(long OpportunityStage_ID)
        {
            /*var OpportunityStage = _context.OpportunityStages.Where(x => x.Id == OpportunityStage_ID).FirstOrDefault();
            OpportunitySatgeModel_ViewModel data = new OpportunitySatgeModel_ViewModel
            {
                Id = OpportunityStage_ID,
                Name = OpportunityStage.Name,
                IsSelected = true
            };
            return data;*/
            return _context.OpportunityStages.Where(x => x.Id == OpportunityStage_ID && x.IsDeleted != true).Select(c => new OpportunitySatgeModelViewModel
            {
                Id = c.Id,
                Name = c.Name,
                IsSelected = true
            }).FirstOrDefault();
        }
    }
}
