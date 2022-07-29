using Microsoft.EntityFrameworkCore;


namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class LobSpaceTypesRepository /*: Repository<LobSpaceType>, ILobSpaceTypesRepository*/
    {
    //    private readonly AppDbContext _context;
    //    public LobSpaceTypesRepository(AppDbContext context) : base(context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<IList<LobSpaceType>> GetAllTypes()
    //    {
    //        var lobSpcaeTypes = await _context.LobSpaceTypes.Where(x => x.IsDeleted == false).ToListAsync();
    //        return lobSpcaeTypes;
    //    }

    //    public async Task<LobSpaceType> GetByName(string name)
    //    {
    //        var lobSpcaeType = await _context.LobSpaceTypes.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Name == name);
    //        return lobSpcaeType;
    //    }

    //    public async Task<bool> LobSpaceTypeExists(long id)
    //    {
    //        return await _context.LobSpaceTypes.AnyAsync(l => l.IsDeleted == false &&
    //                                                          l.Id == id);
    //    }

    //    public async Task UpdatedRelatedContent(long oldLobId, long newLobId, Guid user)
    //    {
    //        await UpdateRelatedCategories(oldLobId, newLobId, user);
    //        await UpdateRelatedFaqs(oldLobId, newLobId, user);
    //        await UpdateRelatedTopUps(oldLobId, newLobId, user);
    //        await UpdateRelatedPlans(oldLobId, newLobId, user);
    //        await UpdateRelatedPolicies(oldLobId, newLobId, user);
    //        await UpdateRelatedWifis(oldLobId, newLobId, user);
    //    }

    //    private async Task UpdateRelatedPlans(long oldLobId, long newLobId, Guid user)
    //    {
    //        await _context.Database
    //            .ExecuteSqlInterpolatedAsync
    //            ($"UPDATE Plans SET LobSpaceTypeId = {newLobId}, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {oldLobId} AND IsDeleted = 0");
    //    }

    //    private async Task UpdateRelatedCategories(long oldLobId, long newLobId, Guid user)
    //    {
    //        await _context.Database
    //            .ExecuteSqlInterpolatedAsync
    //            ($"UPDATE Categories SET LobSpaceTypeId = {newLobId}, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {oldLobId} AND IsDeleted = 0");
    //    }

    //    private async Task UpdateRelatedFaqs(long oldLobId, long newLobId, Guid user)
    //    {
    //        await _context.Database
    //            .ExecuteSqlInterpolatedAsync
    //            ($"UPDATE Faqs SET LobSpaceTypeId = {newLobId}, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {oldLobId} AND IsDeleted = 0");
    //    }

    //    private async Task UpdateRelatedTopUps(long oldLobId, long newLobId, Guid user)
    //    {
    //        await _context.Database
    //            .ExecuteSqlInterpolatedAsync
    //            ($"UPDATE TopUps SET LobSpaceTypeId = {newLobId}, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {oldLobId} AND IsDeleted = 0");
    //    }

    //    private async Task UpdateRelatedPolicies(long oldLobId, long newLobId, Guid user)
    //    {
    //        await _context.Database
    //            .ExecuteSqlInterpolatedAsync
    //            ($"UPDATE Policies SET LobSpaceTypeId = {newLobId}, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {oldLobId} AND IsDeleted = 0");
    //    }

    //    private async Task UpdateRelatedWifis(long oldLobId, long newLobId, Guid user)
    //    {
    //        await _context.Database
    //            .ExecuteSqlInterpolatedAsync
    //            ($"UPDATE Wifis SET LobSpaceTypeId = {newLobId}, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {oldLobId} AND IsDeleted = 0");
    //    }

    //    public async Task DeleteRelatedContent(long lobSpaceTypeId, Guid user)
    //    {
    //        await DeleteRelatedCategories(lobSpaceTypeId, user);
    //        await DeleteRelatedFaqs(lobSpaceTypeId, user);
    //        await DeleteRelatedTopUps(lobSpaceTypeId, user);
    //        await DeleteRelatedPlans(lobSpaceTypeId, user);
    //        await DeleteRelatedPolicies(lobSpaceTypeId, user);
    //        await DeleteRelatedWifis(lobSpaceTypeId, user);
    //    }

    //    private async Task DeleteRelatedPlans(long LobSpaceTypeId, Guid user)
    //    {
    //        await _context.Database
    //           .ExecuteSqlInterpolatedAsync
    //           ($"UPDATE Plans SET IsDeleted = 1, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {LobSpaceTypeId} AND IsDeleted = 0");
    //    }

    //    private async Task DeleteRelatedCategories(long LobSpaceTypeId, Guid user)
    //    {
    //        await _context.Database
    //           .ExecuteSqlInterpolatedAsync
    //           ($"UPDATE Categories SET IsDeleted = 1, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {LobSpaceTypeId} AND IsDeleted = 0");
    //    }

    //    private async Task DeleteRelatedFaqs(long LobSpaceTypeId, Guid user)
    //    {
    //        await _context.Database
    //           .ExecuteSqlInterpolatedAsync
    //           ($"UPDATE Faqs SET IsDeleted = 1, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {LobSpaceTypeId} AND IsDeleted = 0");
    //    }

    //    private async Task DeleteRelatedTopUps(long LobSpaceTypeId, Guid user)
    //    {
    //        await _context.Database
    //           .ExecuteSqlInterpolatedAsync
    //           ($"UPDATE TopUps SET IsDeleted = 1, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {LobSpaceTypeId} AND IsDeleted = 0");
    //    }

    //    private async Task DeleteRelatedPolicies(long LobSpaceTypeId, Guid user)
    //    {
    //        await _context.Database
    //           .ExecuteSqlInterpolatedAsync
    //           ($"UPDATE Policies SET IsDeleted = 1, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {LobSpaceTypeId} AND IsDeleted = 0");
    //    }

    //    private async Task DeleteRelatedWifis(long LobSpaceTypeId, Guid user)
    //    {
    //        await _context.Database
    //           .ExecuteSqlInterpolatedAsync
    //           ($"UPDATE Wifis SET IsDeleted = 1, UpdatedAt = GETDATE(), UpdatedBy = {user} WHERE LobSpaceTypeId = {LobSpaceTypeId} AND IsDeleted = 0");
    //    }
    }
}
