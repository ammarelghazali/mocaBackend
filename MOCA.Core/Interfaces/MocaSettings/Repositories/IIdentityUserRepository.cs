namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IIdentityUserRepository
    {
        Task<string> GetAdminName(Guid guid);
    }
}
