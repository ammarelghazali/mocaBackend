using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IFaqsRepository : IBaseRepository<Faq>, IBaseAllGetableRepository<Faq>
    {
        Task<IEnumerable<Faq>> GetAllFaqsByCategoryAsync(long? spaceId, long categoryId);
        Task<int> GetMaxDisplayOrder(long? spaceId, long? categoryId);
        Task<bool> FaqExists(long? spaceId, long faqId);
        Task<IEnumerable<Faq>> GetNonCategorizedFaqs(long? spaceId);
        Task UpdateFaqsDisplayOrder(long? lobSpaceTypeId, long? categoryId, int displayOrder);
        void UpdateRange(List<Faq> faqs);
        Task<Faq> GetFaqByIdAndLobSpaceId(long? spaceId, long faqId);

    }
}
