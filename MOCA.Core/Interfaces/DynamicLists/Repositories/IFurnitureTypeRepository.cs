

namespace MOCA.Core.Interfaces.DynamicLists.Repositories
{
    public interface IFurnitureTypeRepository
    {
        Task<bool> IsUniqueNameAsync(string setup);
        Task<bool> DeleteFurnitureType(long Id);
    }
}
