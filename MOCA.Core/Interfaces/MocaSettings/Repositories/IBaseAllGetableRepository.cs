namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IBaseAllGetableRepository<T> where T : class
    {
        Task<IList<T>> GetAllBaseAsync(long? spaceId);
    }
}
