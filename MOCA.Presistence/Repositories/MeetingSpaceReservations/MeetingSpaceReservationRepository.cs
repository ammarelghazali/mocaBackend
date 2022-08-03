﻿
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MeetingSpaceReservations
{
    public class MeetingSpaceReservationRepository : GenericRepository<MeetingSpaceReservation>, IMeetingSpaceReservationRepository
    {
        private readonly ApplicationDbContext _context;
        public MeetingSpaceReservationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


    }
}
