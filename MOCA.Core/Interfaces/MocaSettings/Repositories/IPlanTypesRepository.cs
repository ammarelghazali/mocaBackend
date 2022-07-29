using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPlanTypesRepository : IBaseRepository<PlanType>
    {
        Task<PlanType> GetByName(string name);
        Task<IList<PlanType>> GetAllTypes();
    }
}
