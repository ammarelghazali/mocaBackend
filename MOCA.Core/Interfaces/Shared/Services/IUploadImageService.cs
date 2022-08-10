using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Shared.Services
{
    public interface IUploadImageService
    {
        Task<Response<List<string>>> Uploading(ImageUpload image, string fileSetting, string pathType);
    }
}
