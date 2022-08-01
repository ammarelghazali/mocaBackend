using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class EventSpaceBookingRepository : GenericRepository<EventSpaceBooking>, IEventSpaceBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EventSpaceBookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = (ApplicationDbContext)dbContext;
        }
        public async Task<int> BookEventSpace(EventSpaceBooking eventSpaceBooking)
        {
            await _dbContext.Set<EventSpaceBooking>().AddAsync(eventSpaceBooking);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateEventSpaceBookingWebsite(EventSpaceBooking eventSpaceBooking)
        {
            var data = await _dbContext.EventSpaceBookings.Where(eventSpace => eventSpace.Id == eventSpaceBooking.Id
                                                                             && eventSpace.IsDeleted != true).FirstOrDefaultAsync();

            EventSpaceBooking updateData = new EventSpaceBooking
            {
                Id = eventSpaceBooking.Id,
                LocationNameId = eventSpaceBooking.LocationNameId,
                EventRequesterId = eventSpaceBooking.EventRequesterId,
                CompanyCommericalName = eventSpaceBooking.CompanyCommericalName,
                IndustryNameId = eventSpaceBooking.IndustryNameId,
                OtherIndustryName = eventSpaceBooking.OtherIndustryName,
                CompanyWebsite = eventSpaceBooking.CompanyWebsite,
                CompanyFacebook = eventSpaceBooking.CompanyFacebook,
                CompanyLinkedin = eventSpaceBooking.CompanyLinkedin,
                CompanyInstgram = eventSpaceBooking.CompanyInstgram,
                ContactFullName1 = eventSpaceBooking.ContactFullName1,
                ContactMobile1 = eventSpaceBooking.ContactMobile1,
                ContactEmail1 = eventSpaceBooking.ContactEmail1,
                ContactFullName2 = eventSpaceBooking.ContactFullName2,
                ContactMobile2 = eventSpaceBooking.ContactMobile2,
                ContactEmail2 = eventSpaceBooking.ContactEmail2,
                EventName = eventSpaceBooking.EventName,
                EventCategoryId = eventSpaceBooking.EventCategoryId,
                EventDescription = eventSpaceBooking.EventDescription,
                EventReccuranceId = eventSpaceBooking.EventReccuranceId,
                ExpectedNoAttend = eventSpaceBooking.ExpectedNoAttend,
                EventTypeId = eventSpaceBooking.EventTypeId,
                EventAttendanceId = eventSpaceBooking.EventAttendanceId,
                DoesYourEventSupportStartup = eventSpaceBooking.DoesYourEventSupportStartup,
                IsThereThirdPartyOrganizer = eventSpaceBooking.IsThereThirdPartyOrganizer,
                OrgnizingCompany = eventSpaceBooking.OrgnizingCompany,
                CreatedBy = data.CreatedBy,
                CreatedAt = data.CreatedAt,
                LastModifiedBy = data.LastModifiedBy,
                LastModifiedAt = DateTime.UtcNow,
                NeedConsultancy = eventSpaceBooking.NeedConsultancy,
                Platform = eventSpaceBooking.Platform,
                OtherEventCategory = eventSpaceBooking.OtherEventCategory,
                InitiatedId = eventSpaceBooking.InitiatedId,
                IdentityUserId = data.IdentityUserId,
                OpportunityStageId = eventSpaceBooking.OpportunityStageId,
                Revenue = eventSpaceBooking.Revenue,
                SubmissionDate = eventSpaceBooking.SubmissionDate,
                EventOpportunityStatusId = eventSpaceBooking.EventOpportunityStatusId,
                LobLocationTypeId = eventSpaceBooking.LobLocationTypeId
            };
            _dbContext.EventSpaceBookings.Update(updateData);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateEventSpaceBooking(EventSpaceBooking eventSpaceBooking)
        {
            var data = await _dbContext.EventSpaceBookings.Where(eventSpace => eventSpace.Id == eventSpaceBooking.Id &&
                                                                                eventSpace.IsDeleted != true).FirstOrDefaultAsync();

            EventSpaceBooking updateData = new EventSpaceBooking
            {
                Id = eventSpaceBooking.Id,
                LocationNameId = eventSpaceBooking.LocationNameId,
                EventRequesterId = eventSpaceBooking.EventRequesterId,
                CompanyCommericalName = eventSpaceBooking.CompanyCommericalName,
                IndustryNameId = eventSpaceBooking.IndustryNameId,
                OtherIndustryName = eventSpaceBooking.OtherIndustryName,
                CompanyWebsite = eventSpaceBooking.CompanyWebsite,
                CompanyFacebook = eventSpaceBooking.CompanyFacebook,
                CompanyLinkedin = eventSpaceBooking.CompanyLinkedin,
                CompanyInstgram = eventSpaceBooking.CompanyInstgram,
                ContactFullName1 = eventSpaceBooking.ContactFullName1,
                ContactMobile1 = eventSpaceBooking.ContactMobile1,
                ContactEmail1 = eventSpaceBooking.ContactEmail1,
                ContactFullName2 = eventSpaceBooking.ContactFullName2,
                ContactMobile2 = eventSpaceBooking.ContactMobile2,
                ContactEmail2 = eventSpaceBooking.ContactEmail2,
                EventName = eventSpaceBooking.EventName,
                EventCategoryId = eventSpaceBooking.EventCategoryId,
                EventDescription = eventSpaceBooking.EventDescription,
                EventReccuranceId = eventSpaceBooking.EventReccuranceId,
                ExpectedNoAttend = eventSpaceBooking.ExpectedNoAttend,
                EventTypeId = eventSpaceBooking.EventTypeId,
                EventAttendanceId = eventSpaceBooking.EventAttendanceId,
                DoesYourEventSupportStartup = eventSpaceBooking.DoesYourEventSupportStartup,
                IsThereThirdPartyOrganizer = eventSpaceBooking.IsThereThirdPartyOrganizer,
                OrgnizingCompany = eventSpaceBooking.OrgnizingCompany,
                CreatedBy = data.CreatedBy,
                CreatedAt = data.CreatedAt,
                LastModifiedBy = eventSpaceBooking.LastModifiedBy,
                LastModifiedAt = DateTime.UtcNow,
                NeedConsultancy = eventSpaceBooking.NeedConsultancy,
                Platform = eventSpaceBooking.Platform,
                OtherEventCategory = eventSpaceBooking.OtherEventCategory,
                InitiatedId = eventSpaceBooking.InitiatedId,
                IdentityUserId = data.IdentityUserId,
                OpportunityStageId = eventSpaceBooking.OpportunityStageId,
                Revenue = eventSpaceBooking.Revenue,
                SubmissionDate = eventSpaceBooking.SubmissionDate,
                EventOpportunityStatusId = eventSpaceBooking.EventOpportunityStatusId,
                LobLocationTypeId = eventSpaceBooking.LobLocationTypeId
            };
            _dbContext.EventSpaceBookings.Update(updateData);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EventSpaceBooking>> GetAllBookedEventSpaced()
        {
            return await _dbContext.EventSpaceBookings.Where(e => e.IsDeleted != true)
                                                       .OrderByDescending(eventSpace => eventSpace.CreatedAt).AsNoTracking().ToListAsync();
        }

        public async Task<int> CountGetBookedEventSpaceById(long locationTypeId)
        {
            return _dbContext.EventSpaceBookings
                             .Where(eventSpace => eventSpace.LobLocationTypeId == locationTypeId && eventSpace.IsDeleted != true).Count();
        }

        public async Task<List<EventSpaceBooking>> GetAllBookedEventSpaceByLocationId(long locationId)
        {
            return await _dbContext.EventSpaceBookings
                                   .Where(x => x.LocationNameId == locationId && x.IsDeleted != true)
                                   .OrderByDescending(eventSpace => eventSpace.CreatedAt).AsNoTracking().ToListAsync();
        }
        public async Task<List<EventSpaceBooking>> GetAllBookedEventSpaceByLocationTypeId(long locationTypeId)
        {
            return await _dbContext.EventSpaceBookings.Where(x => x.LobLocationTypeId == locationTypeId && x.IsDeleted != true)
                                                       .OrderByDescending(eventSpace => eventSpace.CreatedAt).AsNoTracking().ToListAsync();
        }
        public async Task<List<long>> GetAllDistinctLocation()
        {
            var check = await _dbContext.EventSpaceBookings
                                        .Where(x => x.LocationNameId != null && x.IsDeleted != true)
                                        .Select(c => c.LocationNameId.Value).Distinct().ToListAsync();
            return check;
        }

        public async Task<List<long>> GetAllDistinctRequester(long? locationId)
        {
            return await _dbContext.EventSpaceBookings.Where(x => x.LobLocationTypeId.Value == locationId.Value && x.IsDeleted != true)
                                                       .Select(c => c.EventRequesterId.Value).Distinct().ToListAsync();
        }

        public async Task<List<long>> GetAllDistinctIndusty(long? locationId)
        {
            return await _dbContext.EventSpaceBookings
                                   .Where(c => c.IndustryNameId != 0 && c.IndustryNameId != null &&
                                                c.LocationNameId.Value == locationId.Value && c.IsDeleted != true)
                                   .Select(c => c.IndustryNameId.Value).Distinct().ToListAsync();
        }

        public async Task<List<long>> GetAllDistinctCategory(long? locationId)
        {
            return await _dbContext.EventSpaceBookings
                                   .Where(x => x.EventCategoryId != 0 && x.EventCategoryId != null
                                             && x.LocationNameId.Value == locationId.Value && x.IsDeleted != true)
                                   .Select(c => c.EventCategoryId.Value).Distinct().ToListAsync();
        }

        public async Task<List<long>> GetAllDistinctReccurance(long? locationId)
        {
            return await _dbContext.EventSpaceBookings.Where(x => x.LocationNameId.Value == locationId.Value && x.IsDeleted != true)
                                                       .Select(c => c.EventReccuranceId.Value).Distinct().ToListAsync();
        }

        public async Task<List<long>> GetAllDistinctType(long? locationId)
        {
            return await _dbContext.EventSpaceBookings.Where(x => x.LocationNameId.Value == locationId.Value && x.IsDeleted != true)
                                                       .Select(c => c.EventTypeId.Value).Distinct().ToListAsync();
        }

        public async Task<List<long>> GetAllDistinctAttendance(long? locationId)
        {
            return await _dbContext.EventSpaceBookings.Where(x => x.LocationNameId.Value == locationId.Value && x.IsDeleted != true)
                                                       .Select(c => c.EventAttendanceId.Value).Distinct().ToListAsync();
        }

        public async Task<List<long>> GetAllDistinctInitiated()
        {
            return await _dbContext.EventSpaceBookings.Where(x => x.InitiatedId != null && x.IsDeleted != true)
                                                       .Select(c => c.InitiatedId).Distinct().ToListAsync();
        }

        public async Task<EventSpaceBooking> CheckEmailAndMobileExist(string email, string mobile)
        {
            var UserExist = await _dbContext.EventSpaceBookings.Where(x => x.IsDeleted != true &&
                                                                            ((x.ContactEmail1 == email || x.ContactEmail2 == email)
                                                                            && email != null && email != "" ||
                                                                            (x.ContactMobile1 == mobile || x.ContactMobile2 == mobile)
                                                                            && mobile != null && mobile != "")).FirstOrDefaultAsync();
            if (UserExist == null)
            {
                return UserExist;
            }
            return UserExist;
        }

        public async Task<bool> DeleteEventOpportunitiyByID(long id)
        {
            var EventOpportunitiy = _dbContext.EventSpaceBookings.Where(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
            if (EventOpportunitiy == null)
            {
                return false;
            }
            _dbContext.EventSpaceBookings.Remove(EventOpportunitiy);
            return true;
        }

        public async Task<bool> CheckEventOpportunitiyExistenceByID(long id)
        {
            var EventOpportunitiy = _dbContext.EventSpaceBookings.Where(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
            if (EventOpportunitiy != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateEventOpportunitiyStageReportId(long id, long opportunityStageId)
        {
            var EventOpportunitiy = await _dbContext.EventSpaceBookings.Where(x => x.Id == id && x.IsDeleted != true).FirstOrDefaultAsync();
            if (EventOpportunitiy == null)
            {
                return false;
            }
            EventOpportunitiy.OpportunityStageId = opportunityStageId;
            _dbContext.EventSpaceBookings.Update(EventOpportunitiy);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<EventSpaceBooking>> GetBookedEventSpaceByIdAsync(long locationTypeId, int pageNumber, int pageSize)
        {
            return await _dbContext.EventSpaceBookings
                                 .Where(eventSpace => eventSpace.LobLocationTypeId == locationTypeId && eventSpace.IsDeleted != true)
                                 .Include(ev => ev.EventCategory).Include(ev => ev.EventReccurance)
                                 .Include(ev => ev.EventAttendance).Include(ev => ev.EventRequester)
                                 .Include(ev => ev.EventSpaceTimes).Include(ev => ev.EventSpaceVenues)
                                 .Include(ev => ev.EventType).Include(ev => ev.Initiated)
                                 .Include(ev => ev.OpportunityStage).Include(ev => ev.Initiated)
                                 .Include(ev => ev.EventOpportunityStatus)
                                 .OrderByDescending(eventSpace => eventSpace.CreatedAt)
                                 .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}
