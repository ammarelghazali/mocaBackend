using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.DynamicLists.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.DynamicLists
{
    public class FurnitureTypeRepository : GenericRepository<FurnishingType>, IFurnitureTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public FurnitureTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<bool> DeleteFurnitureType(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUniqueNameAsync(string setup)
        {
            throw new NotImplementedException();
        }
    }
}
