namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IBaseAllGetableWithoutPrarmRepository<T> where T : class
    {
        Task<IList<T>> GetAllBaseAsync();
    }
}
