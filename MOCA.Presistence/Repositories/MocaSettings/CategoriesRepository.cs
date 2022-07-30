using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Category>> GetAllBaseAsync(long? spaceId)
        {
            return await _context.Categories.Where(c => c.LobSpaceTypeId == spaceId &&
                                                        c.IsDeleted != true)
                                            .OrderBy(c => c.DisplayOrder).ToListAsync();
        }

        public async Task<IList<Category>> GetAllCategoriesWithFaqsAsync(long? spaceId)
        {
            return await _context.Categories.Where(c => c.LobSpaceTypeId == spaceId &&
                                                        c.IsDeleted != true)
                                            .OrderBy(c => c.DisplayOrder)
                                            .Include(c => c.Faqs.Where(f => f.IsDeleted != true)
                                            .OrderBy(f => f.DisplayOrder))
                                            .ToListAsync();
        }

        public async Task<Category> GetSingleCategoryWithFaqs(long? spaceId, long categoryId)
        {
            return await _context.Categories.Where(c => c.LobSpaceTypeId == spaceId &&
                                                        c.Id == categoryId &&
                                                        c.IsDeleted != true)
                                            .Include(c => c.Faqs.Where(f => f.IsDeleted != true)
                                                                .OrderBy(f => f.DisplayOrder))
                                            .FirstOrDefaultAsync();
        }

        public async Task DeleteCategory(long? spaceId, long categoryId, bool deleteRelatedQuestions)
        {
            if (!deleteRelatedQuestions)
            {
                await TransferRelatedFaqsToNonCategorizedWithTheRightDisplayOrder(spaceId, categoryId);
            }

            var category = await GetByIdAsync(categoryId);

            //category.IsDeleted = true;
            //category.UpdatedAt = DateTime.UtcNow;
            //category.UpdatedBy = user;

            _context.Categories.Remove(category);

            await UpdateCategoriesDisplayOrder(spaceId, categoryId);

            if (deleteRelatedQuestions)
            {
                var relatedFaqs = await _context.Faqs.Where(x => x.CategoryId == categoryId &&
                                                                 x.IsDeleted != true &&
                                                                 x.LobSpaceTypeId == spaceId &&
                                                                 x.IsDeleted != true)
                                                     .ToListAsync();
                if (relatedFaqs != null)
                {
                    //foreach (var faq in relatedFaqs)
                    //{
                    //    faq.IsDeleted = true;
                    //    faq.UpdatedAt = DateTime.UtcNow;
                    //    faq.UpdatedBy = user;
                    //}

                    _context.Faqs.RemoveRange(relatedFaqs);
                }

                //_context.Faqs.UpdateRange(relatedFaqs);
            }

        }

        public async Task<int> GetMaxDisplayOrder(long? spaceId)
        {
            var order = await _context.Categories
                                            .Where(c => c.LobSpaceTypeId == spaceId && c.IsDeleted != true)
                                            .MaxAsync(c => (int?)c.DisplayOrder) ?? 0;
            return order;
        }

        public async Task<bool> CategoryExists(long? spaceId, long categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId &&
                                                           c.LobSpaceTypeId == spaceId &&
                                                           c.IsDeleted != true);
        }

        private async Task UpdateCategoriesDisplayOrder(long? spaceId, long categoryId)
        {
            var category = await GetByIdAsync(categoryId);

            bool isLast = await GetMaxDisplayOrder(spaceId) == category.DisplayOrder ? true : false;

            if (!isLast)
            {
                //if (spaceId == null)
                //{
                //    await _context.Database
                // .ExecuteSqlInterpolatedAsync
                // ($"UPDATE Category SET DisplayOrder = DisplayOrder - 1 WHERE DisplayOrder > {category.DisplayOrder} AND LobSpaceTypeId IS NULL AND IsDeleted = 0");
                //}
                //else
                //{
                    await _context.Database
                 .ExecuteSqlInterpolatedAsync
                 ($"UPDATE Category SET DisplayOrder = DisplayOrder - 1 WHERE DisplayOrder > {category.DisplayOrder} AND LobSpaceTypeId = {spaceId} AND IsDeleted = 0");
                //}

            }
        }

        private async Task TransferRelatedFaqsToNonCategorizedWithTheRightDisplayOrder(long? spaceId, long categoryId)
        {
            var faqs = await _context.Faqs.Where(f => f.LobSpaceTypeId == spaceId &&
                                                      f.CategoryId == categoryId &&
                                                      f.IsDeleted != true)
                                          .OrderBy(f => f.DisplayOrder)
                                          .ToListAsync();

            var lastDisplayOrder = await _context.Faqs.Where(c => c.LobSpaceTypeId == spaceId &&
                                                                  c.CategoryId == null &&
                                                                  c.IsDeleted != true)
                                           .MaxAsync(c => (int?)c.DisplayOrder) ?? 0;


            foreach (var faq in faqs)
            {
                lastDisplayOrder++;
                faq.DisplayOrder = lastDisplayOrder;
                faq.CategoryId = null;
            }

            _context.Faqs.UpdateRange(faqs);
        }

        public async Task<Category> GetCategoryByIdAndLobSpaceId(long? spaceId, long categoryId)
        {
            return await _context.Categories.Where(c => c.IsDeleted != true &&
                                                        c.LobSpaceTypeId == spaceId &&
                                                        c.Id == categoryId).FirstOrDefaultAsync();
        }

        public async Task<bool> CategoryWithSameNameExist(long? spaceId, string name)
        {
            return await _context.Categories.AnyAsync(c => c.IsDeleted != true &&
                                                           c.LobSpaceTypeId == spaceId &&
                                                           c.Name == name);
        }

        public async Task<bool> UpdateRelatedFaqs(long? lobSpaceTypeId, long? oldCategoryId, long newCategoryId)
        {
            var faqs = await _context.Faqs.Where(f => f.IsDeleted != true &&
                                                      f.CategoryId == oldCategoryId &&
                                                      f.LobSpaceTypeId == lobSpaceTypeId)
                                                  .ToListAsync();


            if (faqs.Count != 0)
            {
                foreach (var faq in faqs)
                {
                    faq.CategoryId = newCategoryId;
                }

                _context.Faqs.UpdateRange(faqs);

                return true;
            }

            return false;
        }
    }
}
