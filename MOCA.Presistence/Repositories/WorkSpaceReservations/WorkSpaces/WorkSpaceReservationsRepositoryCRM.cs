using Dapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories;
using MOCA.Presistence.Contexts;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceReservationsRepositoryCRM : IWorkSpaceReservationsRepositoryCRM
    {
        private readonly ApplicationDbContext _context;
        public WorkSpaceReservationsRepositoryCRM(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns()
        {
            return await _context.ReservationTransactions.Where(r => r.ReservationTypeId == 1 && r.ReservationTypeId == 2 &
                                                                        r.ReservationTypeId == 3 && r.IsDeleted != true &&
                                                                        r.ExtendExpiryDate > DateTime.Now)
                                                            .Include(r => r.Location)
                                                            .Select(r => new DropdownViewModel
                                                            {
                                                                Id = r.LocationId,
                                                                Name = r.Location.Name
                                                            }).Distinct().ToListAsync();

        }
    }
}
