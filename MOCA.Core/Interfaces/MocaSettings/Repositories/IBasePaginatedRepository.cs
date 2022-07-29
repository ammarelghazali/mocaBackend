using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IBasePaginatedRepository<T> where T : class
    {
        Task<PagedResponse<T>> GetPaginatedData(IQueryable<T> source, int pageNumber, int pageSize);
    }
}
