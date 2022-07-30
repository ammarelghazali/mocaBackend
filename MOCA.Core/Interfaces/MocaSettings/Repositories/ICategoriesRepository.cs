using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ICategoriesRepository : IRepository<Category>, IBaseAllGetableRepository<Category>
    {
        Task<IList<Category>> GetAllCategoriesWithFaqsAsync(long? spaceId);
        Task DeleteCategory(long? spaceId, long categoryId, bool deleteRelatedQuestions);
        Task<int> GetMaxDisplayOrder(long? spaceId);
        Task<bool> CategoryExists(long? spaceId, long categoryId);
        Task<bool> CategoryWithSameNameExist(long? spaceId, string name);
        Task<Category> GetSingleCategoryWithFaqs(long? spaceId, long categoryId);
        void UpdateRange(List<Category> categories);
        Task<Category> GetCategoryByIdAndLobSpaceId(long? spaceId, long categoryId);
        Task<bool> UpdateRelatedFaqs(long? lobSpaceTypeId,
                                    long? oldCategoryId, long newCategoryId);
    }
}
