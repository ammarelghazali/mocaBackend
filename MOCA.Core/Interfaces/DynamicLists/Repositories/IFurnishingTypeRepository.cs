

namespace MOCA.Core.Interfaces.DynamicLists.Repositories
{
    public interface IFurnishingTypeRepository
    {
        Task<bool> IsUniqueNameAsync(string setup);
        Task<bool> DeleteFurnitureType(long Id);
    }
}
