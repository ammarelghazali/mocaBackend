﻿using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationTypeRepository : IRepository<LocationType>
    {
        Task<bool> DeleteLocationType(long Id);
    }
}
