using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.DynamicLists.Repositories
{
    public interface IVenueSetupRepository
    {
        Task<bool> IsUniqueNameAsync(string setup);
        Task<bool> DeleteVenueSetup(long Id);
    }
}
