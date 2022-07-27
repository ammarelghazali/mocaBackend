using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.Shared.Exceptions
{
    public class ExistsBeforeException : Exception
    {
        public ExistsBeforeException()
            : base()
        {
        }

        public ExistsBeforeException(string message)
            : base(message)
        {
        }

        public ExistsBeforeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExistsBeforeException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was added before.")
        {
        }
    }
}
