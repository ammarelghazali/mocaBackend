using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.Events;
using MOCA.Core.Interfaces.Events.Repositories;

namespace MOCA.Core
{
    public interface IUnitOfWork : IDisposable
    {
        #region Moca Settings

        #endregion

        #region Events
        IGenericRepository<EventRequester> EventRequesterRepo { get; }
        IGenericRepository<EventCategory> EventCategoryRepo { get; }
        IGenericRepository<EventReccurance> EventReccuranceRepo { get; }
        IGenericRepository<EventAttendance> EventAttendanceRepo { get; }
        IGenericRepository<EventType> EventTypeRepo { get; }
        IContactDetailsRepository ContactDetailsRepo { get; }
        IEventSpaceBookingRepository EventSpaceBookingRepo { get; }
        IEventSpaceTimesRepository EventSpaceTimesRepo { get; }
        IEventSpaceVenueRepository EventSpaceVenuesRepo { get; }
        ISendEmailRepository SendEmailRepo { get; }
        IInitiatedRepository InitiatedRepo { get; }
        IOpportunityStageReportRepository OpportunityStageReportRepo { get; }
        IOpportunityStageRepository OpportunityStageRepo { get; }
        IEventOpportunityStatusRepository EventOpportunityStatusRepo { get; }
        ILocationRepositoty LocationRepo { get; }
        IIndustryRepository IndustryRepo { get; }
        IAccountService AccountService { get; }
        IUserService UserService { get; }
        ILoungeClientRepository LoungeClientRepo { get; }
        ILocationsMemberShipsRepository LocationsMemberShipsRepo { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }

        #endregion

        void Save();
        Task<int> SaveAsync();
        DateTime? GetServerDate();
        DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone);

        DbContext contextForTransaction { get; }
    }
}
