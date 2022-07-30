namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IPasswordEncoderDecoder
    {
        Task<string> DecodePasswordFromBase64(string encodedPassword);
        Task<string> EncodePasswordToBase64(string Password);
    }
}
