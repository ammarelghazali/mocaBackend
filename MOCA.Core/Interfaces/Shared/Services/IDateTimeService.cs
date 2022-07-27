using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.Shared.Services
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
