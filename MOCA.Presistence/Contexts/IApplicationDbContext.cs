using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Contexts
{
    public interface IApplicationDbContext
    {
        public IDbConnection Connection { get; }

        #region Moca Settings

        #endregion

        #region LocationManagment

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
