
namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IAmenityRepository
    {
        Task<bool> IsUniqueNameAsync(string amenityName);
        Task<bool> DeleteAmenity(long Id);
    }
}
