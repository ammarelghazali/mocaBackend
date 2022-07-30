using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class ContactDetailsRepository : GenericRepository<ContactDetails>, IContactDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ContactDetails> CheckEmailAndMobileExist(string email, string mobile)
        {
            var UserExist = await _context.ContactDetails.Where(x => x.IsDeleted != true && (x.Email == email && email != null && email != "" ||
                                                                     x.MobileNumber == mobile && mobile != null && mobile != ""))
                                                                     .FirstOrDefaultAsync();
            if (UserExist == null)
            {
                return UserExist;
            }
            return UserExist;
        }

        public async Task<bool> DeleteContact_DetailByID(long EventsOpportunities_ID)
        {
            List<ContactDetails> ContactDetails = await _context.ContactDetails
                                                                .Where(x => x.EventSpaceBookingId == EventsOpportunities_ID
                                                                         && x.IsDeleted != true)
                                                                .ToListAsync();
            _context.ContactDetails.RemoveRange(ContactDetails);

            return true;
        }

        public async Task<IList<ContactDetails>> GetAllContact_DetailByOpportunitiyID(long EventsOpportunities_ID)
        {
            return await _context.ContactDetails.Where(x => x.EventSpaceBookingId == EventsOpportunities_ID && x.IsDeleted != true)
                                                .ToListAsync();
        }

        public async Task<ContactDetails> GetContact_DetailByID(long Id)
        {
            return await _context.ContactDetails.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
        }
        public async Task<ContactDetails> GetContact_DetailByEmail(string Email)
        {
            return await _context.ContactDetails.Where(x => x.Email == Email && x.IsDeleted != true).FirstOrDefaultAsync();
        }
    }
}
