using Microsoft.Extensions.Options;
using MOCA.Core.Interfaces.Events.Services;
using MOCA.Core.Settings;

namespace MOCA.Services.Implementation.Events
{
    public class FileServices : IFileService
    {
        private readonly FileSettings fileSettings;

        public FileServices(IOptions<FileSettings> FileSettings)
        {
            fileSettings = FileSettings.Value;
        }

    }
}
