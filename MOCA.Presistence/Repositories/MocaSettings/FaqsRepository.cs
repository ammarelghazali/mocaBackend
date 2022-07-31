using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;


namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class FaqsRepository : GenericRepository<Faq>, IFaqsRepository
    {
        private readonly ApplicationDbContext _context;

        public FaqsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Faq>> GetAllBaseAsync(long? spaceId)
        {
            return await _context.Faqs.Where(f => f.LobSpaceTypeId == spaceId &&
                                                 f.IsDeleted != true &&
                                                 f.Category.IsDeleted != true)
                                      .OrderBy(f => f.DisplayOrder).ToListAsync();
        }

        public void UpdateRange(List<Faq> faqs)
        {
            _context.Faqs.UpdateRange(faqs);
        }

        public async Task<IEnumerable<Faq>> GetAllFaqsByCategoryAsync(long? spaceId, long category)
        {
            long? categoryId = category == 0 ? null : category;
            return await _context.Faqs.Where(f => f.CategoryId == categoryId &&
                                                  f.LobSpaceTypeId == spaceId &&
                                                  f.IsDeleted != true)
                                      .OrderBy(f => f.DisplayOrder).ToListAsync();
        }

        public async Task<IEnumerable<Faq>> GetNonCategorizedFaqs(long? spaceId)
        {
            return await _context.Faqs.Where(f => f.LobSpaceTypeId == spaceId &&
                                                  f.CategoryId == null &&
                                                  f.IsDeleted != true)
                                      .OrderBy(f => f.DisplayOrder).ToListAsync();
        }

        public async Task<int> GetMaxDisplayOrder(long? spaceId, long? category)
        {
            var categoryId = category == 0 ? null : category;
            var order = await _context.Faqs.Where(f => f.LobSpaceTypeId == spaceId &&
                                                       f.CategoryId == categoryId
                                                       && f.IsDeleted != true)
                                           .MaxAsync(c => (int?)c.DisplayOrder) ?? 0;
            return order;
        }

        public async Task<bool> FaqExists(long? spaceId, long faqId)
        {
            return await _context.Faqs.AnyAsync(f => f.Id == faqId && f.LobSpaceTypeId == spaceId
                                                     && f.IsDeleted != true);
        }

        public async Task UpdateFaqsDisplayOrder(long? lobSpaceTypeId,
                                                 long? categoryId, int displayOrder)
        {
            if (categoryId != null && lobSpaceTypeId != null)
            {
                await _context.Database
                .ExecuteSqlInterpolatedAsync
                ($"UPDATE Faq SET DisplayOrder = DisplayOrder - 1 WHERE DisplayOrder > {displayOrder} AND CategoryId = {categoryId} AND LobSpaceTypeId = {lobSpaceTypeId} AND IsDeleted = 0");
            }

            else if (categoryId != null && lobSpaceTypeId == null)
            {
                await _context.Database
                .ExecuteSqlInterpolatedAsync
                ($"UPDATE Faq SET DisplayOrder = DisplayOrder - 1 WHERE DisplayOrder > {displayOrder} AND CategoryId = {categoryId} AND LobSpaceTypeId IS NULL AND IsDeleted = 0");
            }

            else if (categoryId == null && lobSpaceTypeId != null)
            {
                await _context.Database
                .ExecuteSqlInterpolatedAsync
                ($"UPDATE Faq SET DisplayOrder = DisplayOrder - 1 WHERE DisplayOrder > {displayOrder} AND CategoryId IS NULL  AND LobSpaceTypeId = {lobSpaceTypeId} AND IsDeleted = 0");
            }

            else
            {
                await _context.Database
                .ExecuteSqlInterpolatedAsync
                ($"UPDATE Faq SET DisplayOrder = DisplayOrder - 1 WHERE DisplayOrder > {displayOrder} AND CategoryId IS NULL  AND LobSpaceTypeId IS NULL AND IsDeleted = 0");
            }
        }

        public async Task<Faq> GetFaqByIdAndLobSpaceId(long? spaceId, long faqId)
        {
            return await _context.Faqs.Where(f => f.IsDeleted != true &&
                                                  f.LobSpaceTypeId == spaceId &&
                                                  f.Id == faqId).FirstOrDefaultAsync();
        }
    }
}
