using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class MeetingSpaceRepository : GenericRepository<MeetingSpace>, IMeetingSpaceRepository
    {
        public MeetingSpaceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
