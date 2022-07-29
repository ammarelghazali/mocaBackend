﻿using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ICategoriesRepository : IBaseRepository<Category>, IBaseAllGetableRepository<Category>
    {
        Task<IList<Category>> GetAllCategoriesWithFaqsAsync(long? spaceId);
        Task DeleteCategory(long? spaceId, long categoryId, bool deleteRelatedQuestions, Guid user);
        Task<int> GetMaxDisplayOrder(long? spaceId);
        Task<bool> CategoryExists(long? spaceId, long categoryId);
        Task<bool> CategoryWithSameNameExist(long? spaceId, string name);
        Task<Category> GetSingleCategoryWithFaqs(long? spaceId, long categoryId);
        void UpdateRange(List<Category> categories);
        Task<Category> GetCategoryByIdAndLobSpaceId(long? spaceId, long categoryId);
        Task<bool> UpdateRelatedFaqs(long? lobSpaceTypeId,
                                    long? oldCategoryId, long newCategoryId, Guid user);
    }
}
