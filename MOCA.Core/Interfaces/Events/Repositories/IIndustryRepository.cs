using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IIndustryRepository
    {
        Task<Industry> GetByID(long? id);
    }
}
