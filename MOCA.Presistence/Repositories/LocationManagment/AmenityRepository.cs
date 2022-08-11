using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _context;
        public AmenityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsUniqueNameAsync(string amenityName)
        {
            var workSpaceType = _context.WorkSpaceTypes.Where(x => x.Name.Equals(amenityName) && x.IsDeleted != true).FirstOrDefault();
            if (workSpaceType == null)
            {
                return true;
            }
            return false;
        }
    }
}
