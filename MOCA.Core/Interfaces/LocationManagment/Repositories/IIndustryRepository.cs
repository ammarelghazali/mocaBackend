﻿using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IIndustryRepository : IGenericRepository<Industry>
    {
        Task<bool> DeleteIndustry(long Id);
        Task<bool> HasAnyRelatedEntities(long IndustryID);
    }
}
