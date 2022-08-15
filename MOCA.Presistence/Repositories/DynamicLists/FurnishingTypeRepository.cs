using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.DynamicLists.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;


namespace MOCA.Presistence.Repositories.DynamicLists
{
    public class FurnishingTypeRepository : GenericRepository<FurnishingType>, IFurnishingTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public FurnishingTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteFurnitureType(long Id)
        {
            var Furnishing = _context.FurnishingTypes.Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
            if (Furnishing == null)
            {
                return false;
            }
            _context.FurnishingTypes.Remove(Furnishing);
            return true;
        }

        public async Task<bool> IsUniqueNameAsync(string furnishing)
        {
            var Furnishing = _context.FurnishingTypes.Where(x => x.Name.Equals(furnishing) && x.IsDeleted != true).FirstOrDefault();
            if (Furnishing == null)
            {
                return true;
            }
            return false;
        }
    }
}
