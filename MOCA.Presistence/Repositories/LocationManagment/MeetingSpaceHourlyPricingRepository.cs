using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class MeetingSpaceHourlyPricingRepository : GenericRepository<MeetingSpaceHourlyPricing>, IMeetingSpaceHourlyPricingRepository
    {
        public MeetingSpaceHourlyPricingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
