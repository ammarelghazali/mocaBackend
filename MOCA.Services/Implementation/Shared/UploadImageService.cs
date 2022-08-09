using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Shared.Services;
using System.Drawing;

namespace MOCA.Services.Implementation.Shared
{
    public class UploadImageService : IUploadImageService
    {
        public UploadImageService()
        {
            
        }

        public async Task<Response<List<string>>> Uploading(ImageUpload image, string fileSetting, string pathType)
        {
            try
            {
                string? folderName = fileSetting + "//" + image.AlbumName.ToLower();
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                List<string> lstFileNames = new List<string>();
                var dbPath = "";

                //save image
                if (!string.IsNullOrEmpty(image.Image))
                {
                    try
                    {
                        var folderNames = folderName;
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderNames);

                        byte[] bytes = Convert.FromBase64String(image.Image);

                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            Image pic = Image.FromStream(ms);

                            string renameFile = DateTime.UtcNow.Ticks + "_" + pathType + "." + pic.RawFormat.ToString();
                            var fullPath = Path.Combine(pathToSave, renameFile);
                            dbPath = Path.Combine(folderName, renameFile);

                            pic.Save(fullPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new Response<List<string>>("Couldn't Upload Image");
                    }
                    string imagePath = dbPath;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        lstFileNames.Add(imagePath);
                        return new Response<List<string>>(lstFileNames);
                    }
                }
                else
                {
                    return new Response<List<string>>("Couldn't Upload Image");
                }
            }
            catch (Exception ex)
            {
                return new Response<List<string>>($"Internal server error: {ex}");
            }
            return new Response<List<string>>("Couldn't Upload Image.");
        }
    }
}
