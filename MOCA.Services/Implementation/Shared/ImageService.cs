using MOCA.Core.Interfaces.Shared.Services.ThirdParty.Winfi;
using System;
using System.Drawing;
using System.IO;

namespace MOCA.Services.Implementation.Shared
{
    public class ImageService : IImageService
    {
        public string Base64ToImage(string imagestr, string folderName, string partOfRenameFile)
        {
            try
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                byte[] bytes = Convert.FromBase64String(imagestr);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image pic = Image.FromStream(ms);

                    string renameFile = DateTime.UtcNow.Ticks + "_" + partOfRenameFile + "." + pic.RawFormat.ToString();
                    var fullPath = Path.Combine(pathToSave, renameFile);
                    var dbPath = Path.Combine(folderName, renameFile);

                    pic.Save(fullPath);
                    return dbPath;
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }


}
