using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPlanTypesRepository : IRepository<PlanType>
    {
        Task<PlanType> GetByName(string name);
        Task<IList<PlanType>> GetAllTypes();
    }
}
