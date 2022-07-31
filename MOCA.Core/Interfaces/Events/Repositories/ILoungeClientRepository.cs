namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface ILoungeClientRepository
    {
        Task<string> GetClientNameById(long Client_Id);
    }
}
