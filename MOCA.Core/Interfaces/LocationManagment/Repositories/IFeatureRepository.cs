
namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IFeatureRepository
    {
        Task<bool> DeleteFeature(long Id);
        Task<bool> HasAnyRelatedEntities(long FeatureID);
    }
}
