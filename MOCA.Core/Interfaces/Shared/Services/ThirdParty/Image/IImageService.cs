namespace MOCA.Core.Interfaces.Shared.Services.ThirdParty.Winfi
{
    public interface IImageService
    {
        string Base64ToImage(string imagestr,string folderName,string partOfRenameFile);
    }
}
