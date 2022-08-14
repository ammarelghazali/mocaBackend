using Dapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Repositories;
using MOCA.Presistence.Contexts;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations.CoworkSpace
{
    public class CoworkSpaceReservationsRepositoryCRM : ICoworkSpaceReservationsRepositoryCRM
    {
        private readonly ApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public CoworkSpaceReservationsRepositoryCRM(ApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }

        public async Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns()
        {
            return await _context.ReservationTransactions.Where(r => r.ReservationTypeId == 3 && r.ReservationTypeId == 4 &
                                                                                    r.ReservationTypeId == 5 && r.IsDeleted != true &&
                                                                                    r.ExtendExpiryDate > _dateTimeService.NowUtc)
                                                                        .Include(r => r.Location)
                                                                        .Select(r => new DropdownViewModel
                                                                        {
                                                                            Id = r.LocationId,
                                                                            Name = r.Location.Name
                                                                        }).Distinct().ToListAsync();
        }
    }
}
