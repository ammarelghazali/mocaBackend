using System.Text;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Options;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventOpportunityDtos;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Request;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.Shared.ThirdParty.Email;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Events.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.Shared.Services.ThirdParty.Email;
using MOCA.Core.Settings;

namespace MOCA.Services.Implementation.Events
{
    public class EventOpportunityService : IEventOpportunityService
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IPasswordEncoderDecoder _passwordEncoderDecoder;
        private readonly IEmailService _emailService;
        private readonly MailSettings _mailSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;

        public EventOpportunityService(IAuthenticatedUserService authenticatedUser,
                                       IMapper mapper,
                                       IPasswordEncoderDecoder _passwordEncoderDecoder,
                                       IOptions<MailSettings> mailSettings,
                                       IEmailService emailService,
                                       IUnitOfWork unitOfWork,
                                       IDateTimeService dateTimeService)
        {
            _authenticatedUser = authenticatedUser ?? throw new ArgumentNullException(nameof(authenticatedUser));
            this._passwordEncoderDecoder = _passwordEncoderDecoder ?? throw new ArgumentNullException(nameof(_passwordEncoderDecoder));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(_dateTimeService));
            _mailSettings = mailSettings.Value;
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<long>> CreateNewOpportunity(cmd_Create_NewEventOpportunity_Parameter request)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }

                foreach (var item in request.ContactDetails)
                {
                    var checkExistanceEmailOrMobileExistInEventspaceBooking = await _unitOfWork.EventSpaceBookingRepo.CheckEmailAndMobileExist(item.Email, item.MobileNumber);
                    var checkExistanceEmailOrMobileExistInContactDetails = await _unitOfWork.ContactDetailsRepo.CheckEmailAndMobileExist(item.Email, item.MobileNumber);
                    if (checkExistanceEmailOrMobileExistInEventspaceBooking != null)
                    {
                        return new Response<long>($"This user has a previous opportunities with id {checkExistanceEmailOrMobileExistInEventspaceBooking.Id}");
                    }
                    if (checkExistanceEmailOrMobileExistInContactDetails != null)
                    {
                        return new Response<long>($"This user has a previous opportunities with id {checkExistanceEmailOrMobileExistInContactDetails.EventSpaceBookingId}");

                    }
                }

                var locTypeExists = await _unitOfWork.LocationTypeRepo.GetByIdAsync((int)request.LobLocationTypeId);

                if (locTypeExists == null)
                {
                    return new Response<long>($"location type {request.LobLocationTypeId} is not exists");
                }

                if (await _unitOfWork.EventRequesterRepo.GetByIdAsync(request.EventRequesterId) is null)
                {
                    return new Response<long>($"Event Requester {request.EventRequesterId} is not exists");
                }

                var opportunity = _mapper.Map<EventSpaceBooking>(request);
                opportunity.SubmissionDate = _dateTimeService.NowUtc;
                opportunity.IdentityUserId = _authenticatedUser.UserId;
                opportunity.Platform = "copolitan";


                await _unitOfWork.EventSpaceBookingRepo.AddAsync(opportunity);

                foreach (var item in request.ContactDetails)
                {
                    var contact = new ContactDetails
                    {
                        Name = item.Name,
                        Email = item.Email,
                        MobileNumber = item.MobileNumber,
                        EventSpaceBookingId = opportunity.Id
                    };
                    await _unitOfWork.ContactDetailsRepo.AddAsync(contact);
                }

                return new Response<long>(opportunity.Id, "Opportunitiy Created Successfully.");
            }
            catch (Exception ex)
            {
                return new Response<long>(ex.ToString());
            }
        }

        public async Task<Response<bool>> DeleteOpportunity(cmd_Delete_EventOpportunity_Parameter request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var opportunity = await _unitOfWork.EventSpaceBookingRepo.DeleteEventOpportunitiyByID(request.EventOpportunityID);

            if (opportunity == false)
                return new Response<bool>("Event Opportunitiy With This ID didn't exist.");

            var contactDetails = await _unitOfWork.ContactDetailsRepo.DeleteContact_DetailByID(request.EventOpportunityID);

            if (contactDetails == false)
                return new Response<bool>("Event Opportunitiy contact details of that With This ID didn't exist.");


            await _unitOfWork.EventSpaceTimesRepo.DeleteByBookEventSpace_ID(request.EventOpportunityID);
            await _unitOfWork.EventSpaceVenuesRepo.DeleteByBookEventSpace_ID(request.EventOpportunityID);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Opportunity right now");
            }

            return new Response<bool>(true, "Event Opportunitiy Deleted Successfully.");
        }

        public async Task<PagedResponse<IList<EventOpportunityDetails_Send_ViewModel>>> FilterWithPagination(cmd_Filter_EventOpportunityDetails_WithPagination_Query request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", request.Id);
            parms.Add("@FromSubmissionDate", request.FromSubmissionDate);
            parms.Add("@ToSubmissionDate", request.ToSubmissionDate);
            parms.Add("@Requester", request.Requester);
            parms.Add("@Initiated", request.Initiated);
            parms.Add("@Name", request.Name);
            parms.Add("@OwnerName", request.OwnerName);
            parms.Add("@LocationTypeId", request.LocationType_ID);
            parms.Add("@pageNumber", request.pageNumber);
            parms.Add("@pageSize", request.pageSize);
            var res = await _unitOfWork.EventSpaceBookingRepo.QueryAsync<EventsOpportunitiesSearch_ViewModel>("[dbo].[SP_EventsOpportunities_Search]", parms, System.Data.CommandType.StoredProcedure);
            var EventOpportunityDetailsWithoutPagination = new List<EventOpportunityDetails_Send_ViewModel>();

            foreach (var item in res)
            {
                var EventOpportunityDetails = new EventOpportunityDetails_Send_ViewModel();

                var ContactDetails = await _unitOfWork.ContactDetailsRepo.GetAllContact_DetailByOpportunitiyID(item.Id);
                var EmailsHistory = await _unitOfWork.SendEmailRepo.GetEmailHistoryByOpportunitiyID(item.Id);
                var EventRequest = _unitOfWork.EventRequesterRepo.GetByID(item.EventRequesterId);
                var initiat = await _unitOfWork.InitiatedRepo.GetByIdAsync(item.InitiatedId);

                EventOpportunityDetails = new EventOpportunityDetails_Send_ViewModel
                {
                    Opportunity_ID = item.Id,
                    SubmissionDate = (DateTime)item.SubmissionDate,
                    OpportunityOwner = _authenticatedUser.UserName,
                    Initiated = new Initiated
                    {
                        Id = initiat.Id,
                        Name = initiat.Name,
                    },
                    EventRequester_ID = (long)item.EventRequesterId,
                    CompanyName = item.CompanyName,
                    EventRequester_Name = EventRequest.Name,
                    ContactDetails = _mapper.Map<List<EventOpportunityContactDetails_ViewModel>>(ContactDetails),
                    SendEmails = new List<SendEmail>(EmailsHistory)
                };
                EventOpportunityDetailsWithoutPagination.Add(EventOpportunityDetails);
            }
            // var Resultpaginated = EventOpportunityDetailsWithoutPagination.Skip( (request.pageNumber - 1) * request.pageSize ).Take(request.pageSize).ToList();
            if (EventOpportunityDetailsWithoutPagination.Count > 0)
            {
                return new PagedResponse<IList<EventOpportunityDetails_Send_ViewModel>>(EventOpportunityDetailsWithoutPagination, request.pageNumber, request.pageSize, res[0].pg_total);
            }

            return new PagedResponse<IList<EventOpportunityDetails_Send_ViewModel>>(null, request.pageNumber, request.pageSize);
        }



        public async Task<Response<cmd_Get_DetailedEventOpportunity_ViewModel>> GetEventOpportunityDetails(cmd_Get_DetailedEventOpportunity_Parameter request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var eventSpaceBooking = await _unitOfWork.EventSpaceBookingRepo.GetByIdAsync(request.OpportunityID);
            if (eventSpaceBooking == null || eventSpaceBooking.IsDeleted)
            {
                return new Response<cmd_Get_DetailedEventOpportunity_ViewModel>("There is no submission with this Id.");
            }

            List<OpportunityStageReport> opportunityStageReport = new List<OpportunityStageReport>();
            if (eventSpaceBooking.OpportunityStageId != null)
            {
                opportunityStageReport = await _unitOfWork.OpportunityStageReportRepo.GetReportByIDs(request.OpportunityID);
            }

            var opportunityStageList = await _unitOfWork.OpportunityStageRepo.GetDefaultStage();
            if (!(eventSpaceBooking.OpportunityStageId >= 1 && eventSpaceBooking.OpportunityStageId <= 7) && eventSpaceBooking.OpportunityStageId != null)
            {
                var stage = await _unitOfWork.OpportunityStageRepo.GetCurrentStage((long)eventSpaceBooking.OpportunityStageId);
                opportunityStageList.Add(stage);
            }
            else if (eventSpaceBooking.OpportunityStageId >= 1 && eventSpaceBooking.OpportunityStageId <= 7 && eventSpaceBooking.OpportunityStageId != null)
            {
                opportunityStageList.Where(o => o.Id == eventSpaceBooking.OpportunityStageId).FirstOrDefault().IsSelected = true;
                //int index = (int)eventSpaceBooking.OpportunityStage_ID;
                //opportunityStageList[index - 1].IsSelected = true;
            }

            OpportunityInfo_ViewModel opportunityInfo_ViewModel = new OpportunityInfo_ViewModel
            {
                OpportunityID = eventSpaceBooking.Id,
                LOS = "Eventspace",
                SubmissionDate = eventSpaceBooking.SubmissionDate != null ? eventSpaceBooking.SubmissionDate.Value.ToShortDateString() : null,
                OpportunityOwner = eventSpaceBooking.IdentityUserId != null ? eventSpaceBooking.IdentityUserId : null,
                MembershipStatus = eventSpaceBooking.Platform != null ? eventSpaceBooking.Platform : null,
                OpportunityStage = opportunityStageList
            };

            List<opportunityStageReport_ViewModel> opportunityStageReportData = new List<opportunityStageReport_ViewModel>();
            if (opportunityStageReport.Count > 0)
            {
                foreach (var item in opportunityStageReport)
                {
                    opportunityStageReportData.Add(new opportunityStageReport_ViewModel
                    {
                        Date = item.Date.ToShortDateString(),
                        OpportunityUpdate = item.Comment,
                        Reminder = item.Reminder.Value.ToShortDateString()
                    });
                }
            }

            var requester = _unitOfWork.EventRequesterRepo.GetByID(eventSpaceBooking.EventRequesterId);
            Location location = new Location();
            if (eventSpaceBooking.LocationNameId != null)
            {
                //return new Response<cmd_Get_DetailedEventOpportunity_ViewModel>("Please Complete your submission.");
                location = await _unitOfWork.LocationsMemberShipsRepo.GetLocationByID((long)eventSpaceBooking.LocationNameId);
            }

            General_ViewModel general_ViewModel = null;
            if (eventSpaceBooking.LocationNameId == null)
            {
                general_ViewModel = new General_ViewModel
                {
                    Location = null,
                    EventRequester = new Model_ViewModel { Id = requester.Id, Name = requester.Name }
                };
            }
            else
            {
                general_ViewModel = new General_ViewModel
                {
                    Location = new Model_ViewModel { Id = location.Id, Name = location.Name },
                    EventRequester = new Model_ViewModel { Id = requester.Id, Name = requester.Name }
                };
            }

            CompanyInfo_ViewModel companyInfo_ViewModel = null;
            if (eventSpaceBooking.IndustryNameId != null)
            {
                var industry = await _unitOfWork.IndustryRepo.GetByIdAsync((int)eventSpaceBooking.IndustryNameId);
                companyInfo_ViewModel = new CompanyInfo_ViewModel
                {
                    CompanyName = eventSpaceBooking.CompanyCommericalName,
                    Facebook = eventSpaceBooking.CompanyFacebook,
                    Industry = new Model_ViewModel { Id = industry.Id, Name = industry.Name },
                    Instagram = eventSpaceBooking.CompanyInstgram,
                    LinkedIn = eventSpaceBooking.CompanyLinkedin,
                    Website = eventSpaceBooking.CompanyWebsite
                };
            }
            else
            {
                companyInfo_ViewModel = new CompanyInfo_ViewModel
                {
                    CompanyName = eventSpaceBooking.CompanyCommericalName,
                    Facebook = eventSpaceBooking.CompanyFacebook,
                    Industry = null,
                    Instagram = eventSpaceBooking.CompanyInstgram,
                    LinkedIn = eventSpaceBooking.CompanyLinkedin,
                    Website = eventSpaceBooking.CompanyWebsite
                };
            }

            IndividualDetails_ViewModel individualDetails_ViewModel = new IndividualDetails_ViewModel
            {
                FirstContact = new Contact_ViewModel { Name = eventSpaceBooking.ContactFullName1, Email = eventSpaceBooking.ContactEmail1, Mobile = eventSpaceBooking.ContactMobile1 },
                SecondContact = new Contact_ViewModel { Name = eventSpaceBooking.ContactFullName2, Email = eventSpaceBooking.ContactEmail2, Mobile = eventSpaceBooking.ContactMobile2 }
            };

            EventCategory category = null;
            if (eventSpaceBooking.EventCategoryId != null)
            {
                category = _unitOfWork.EventCategoryRepo.GetByID(eventSpaceBooking.EventCategoryId);
            }

            EventReccurance reccurance = null;
            if (eventSpaceBooking.EventReccuranceId != null)
            {
                reccurance = _unitOfWork.EventReccuranceRepo.GetByID(eventSpaceBooking.EventReccuranceId);
            }

            var times = await _unitOfWork.EventSpaceTimesRepo.GetBookedEventSpaceTimeById(eventSpaceBooking.Id);
            var venues = await _unitOfWork.EventSpaceVenuesRepo.GetEventSpaceVenuesById(eventSpaceBooking.Id);


            List<Model_ViewModel> venueData = new List<Model_ViewModel>();
            foreach (var item in venues)
            {
                venueData.Add(new Model_ViewModel
                {
                    Id = item.Id,
                    Name = item.VenueName
                });
            }

            EventType type = null;
            if (eventSpaceBooking.EventTypeId != null)
            {
                type = _unitOfWork.EventTypeRepo.GetByID(eventSpaceBooking.EventTypeId);
            }

            EventAttendance attend = null;
            if (eventSpaceBooking.EventAttendanceId != null)
            {
                attend = _unitOfWork.EventAttendanceRepo.GetByID(eventSpaceBooking.EventAttendanceId);
            }

            EventDetails_ViewModel eventDetails_ViewModel = new EventDetails_ViewModel();
            if (category != null && reccurance != null && type != null && attend != null)
            {
                eventDetails_ViewModel = new EventDetails_ViewModel
                {
                    EventName = eventSpaceBooking.EventName,
                    EventCategory = new Model_ViewModel { Id = category.Id, Name = category.Name },
                    EventRecurrence = new Model_ViewModel { Id = reccurance.Id, Name = reccurance.Name },
                    eventTimes = times == null ? null : times,
                    ExpectedNumberOfAttendees = eventSpaceBooking.ExpectedNoAttend == null ? null : eventSpaceBooking.ExpectedNoAttend,
                    PreferredVenue = venueData == null ? null : venueData,
                    EventType = new Model_ViewModel { Id = type.Id, Name = type.Name },
                    EventAttendance = new Model_ViewModel { Id = attend.Id, Name = attend.Name },
                    EventSupportStartups = eventSpaceBooking.DoesYourEventSupportStartup == null ? null : eventSpaceBooking.DoesYourEventSupportStartup,
                    PartyOrganizer = eventSpaceBooking.IsThereThirdPartyOrganizer == null ? null : eventSpaceBooking.IsThereThirdPartyOrganizer,
                    OrganizerName = eventSpaceBooking.OrgnizingCompany,
                    EventDescription = eventSpaceBooking.EventDescription
                };
            }
            else
            {
                eventDetails_ViewModel = new EventDetails_ViewModel
                {
                    EventName = eventSpaceBooking.EventName,
                    EventCategory = null,
                    EventRecurrence = null,
                    eventTimes = times == null ? null : times,
                    ExpectedNumberOfAttendees = eventSpaceBooking.ExpectedNoAttend == null ? null : eventSpaceBooking.ExpectedNoAttend,
                    PreferredVenue = venueData == null ? null : venueData,
                    EventType = null,
                    EventAttendance = null,
                    EventSupportStartups = eventSpaceBooking.DoesYourEventSupportStartup == null ? null : eventSpaceBooking.DoesYourEventSupportStartup,
                    PartyOrganizer = eventSpaceBooking.IsThereThirdPartyOrganizer == null ? null : eventSpaceBooking.IsThereThirdPartyOrganizer,
                    OrganizerName = eventSpaceBooking.OrgnizingCompany,
                    EventDescription = eventSpaceBooking.EventDescription
                };
            }

            cmd_Get_DetailedEventOpportunity_ViewModel data = new cmd_Get_DetailedEventOpportunity_ViewModel
            {
                OpportunityInfo = opportunityInfo_ViewModel,
                opportunityStageReport = opportunityStageReportData,
                General = general_ViewModel,
                CompanyInfo = companyInfo_ViewModel,
                IndividualDetails = individualDetails_ViewModel,
                EventDetails = eventDetails_ViewModel
            };

            if (data.OpportunityInfo.MembershipStatus == "Mobile")
            {
                //var loungeClient = await _unitOfWork.LoungeClientRepo.GetClientNameById(long.Parse(data.OpportunityInfo.OpportunityOwner));
               // data.OpportunityInfo.OpportunityOwner = loungeClient;
            }
            else if (data.OpportunityInfo.MembershipStatus == "copolitan")
            {
                var admin = await _unitOfWork.UserService.GetUserByID(data.OpportunityInfo.OpportunityOwner);
                data.OpportunityInfo.OpportunityOwner = admin.Data.FirstName + " " + admin.Data.LastName;
            }

            return new Response<cmd_Get_DetailedEventOpportunity_ViewModel>(data);
        }



        public async Task<Response<EventOpportunityDetails_ViewModel>> GetOpportunityDetailsByEventOpportunityID(cmd_Get_EventOpportunityDetails_Parameter request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var EventsOpportunitiy = await _unitOfWork.EventSpaceBookingRepo.GetByIdAsync(request.Opportunity_ID);
            if (EventsOpportunitiy == null || EventsOpportunitiy.IsDeleted)
            {
                return new Response<EventOpportunityDetails_ViewModel>("There is no submission for this Id.");
            }
            var ContactDetails = await _unitOfWork.ContactDetailsRepo.GetAllContact_DetailByOpportunitiyID(request.Opportunity_ID);
            var SendEmailsHistory = await _unitOfWork.SendEmailRepo.GetEmailHistoryByOpportunitiyID(request.Opportunity_ID);

            var EventOpportunityDetails = new EventOpportunityDetails_ViewModel
            {
                Opportunity_ID = request.Opportunity_ID,
                SubmissionDate = (DateTime)EventsOpportunitiy.SubmissionDate,
                OpportunityOwner = _authenticatedUser.UserName,
                EventRequester_ID = (long)EventsOpportunitiy.EventRequesterId,
                CompanyName = EventsOpportunitiy.CompanyCommericalName,
            };

            EventOpportunityDetails.ContactDetails = new List<EventOpportunityContactDetails_ViewModel>();
            EventOpportunityDetails.EmailTemplate = new List<SendEmail_ViewModel>();

            // Iterate over all send email history, and the emails with same email template and date considered to be the same email
            // but sent to different contact detail, so we need to check that and add all the contact details of the specific email
            // otherwise it's considered a new email with new email template and contact details
            foreach (var items in SendEmailsHistory)
            {
                var check = EventOpportunityDetails.EmailTemplate
                                                   .Where(x => x.Subject == items.Subject
                                                            && x.Body == items.Body
                                                            && x.EmailTemplate_ID == items.EmailTemplateId
                                                            && x.CreatedAt.ToString("yyyy-MM-dd HH:mm") == ((DateTime)items.CreatedAt)
                                                            .ToString("yyyy-MM-dd HH:mm")).FirstOrDefault();

                EventOpportunityContactDetails_ViewModel eventOpportunityContactDetails_ViewModel = new EventOpportunityContactDetails_ViewModel();

                if (check != null)
                {
                    var contact = await _unitOfWork.ContactDetailsRepo.GetByIdAsync(items.ContactDetailId);
                    eventOpportunityContactDetails_ViewModel.id = contact.Id;
                    eventOpportunityContactDetails_ViewModel.email = contact.Email;
                    eventOpportunityContactDetails_ViewModel.mobileNumber = contact.MobileNumber;
                    eventOpportunityContactDetails_ViewModel.name = contact.Name;
                    var checkcontact = check.ContactDetails.Where(x => x.id == contact.Id).FirstOrDefault();
                    if (checkcontact == null)
                    {
                        check.ContactDetails.Add(eventOpportunityContactDetails_ViewModel);
                    }
                }
                else
                {
                    SendEmail_ViewModel SendEmailBycontact = new SendEmail_ViewModel();
                    SendEmailBycontact.ContactDetails = new List<EventOpportunityContactDetails_ViewModel>();
                    SendEmailBycontact.Id = items.Id;
                    SendEmailBycontact.Subject = items.Subject;
                    SendEmailBycontact.Body = items.Body;
                    SendEmailBycontact.CC = items.CC;
                    SendEmailBycontact.FromUser = items.FromUser;
                    SendEmailBycontact.CreatedAt = (DateTime)items.CreatedAt;
                    SendEmailBycontact.EmailTemplate_ID = items.EmailTemplateId;

                    var contact = await _unitOfWork.ContactDetailsRepo.GetByIdAsync(items.ContactDetailId);
                    if (contact != null)
                    {
                        eventOpportunityContactDetails_ViewModel.id = contact.Id;
                        eventOpportunityContactDetails_ViewModel.email = contact.Email;
                        eventOpportunityContactDetails_ViewModel.mobileNumber = contact.MobileNumber;
                        eventOpportunityContactDetails_ViewModel.name = contact.Name;
                    }

                    SendEmailBycontact.ContactDetails.Add(eventOpportunityContactDetails_ViewModel);
                    EventOpportunityDetails.EmailTemplate.Add(SendEmailBycontact);
                }
            }
            foreach (var item in ContactDetails)
            {
                EventOpportunityDetails.ContactDetails.Add(new EventOpportunityContactDetails_ViewModel
                {
                    id = item.Id,
                    name = item.Name,
                    email = item.Email,
                    mobileNumber = item.MobileNumber
                });
            }

            return new Response<EventOpportunityDetails_ViewModel>(EventOpportunityDetails);
        }



        public async Task<Response<IList<EventOpportunityDetails_Send_ViewModel>>> GetOpportunityDetailsWithoutPagination(long locationTypeId)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var EventOpportunityDetailsWithoutPagination = new List<EventOpportunityDetails_Send_ViewModel>();
            var AllEventsOpportunitiy = await _unitOfWork.EventSpaceBookingRepo.GetAllBookedEventSpaceByLocationTypeId(locationTypeId);

            foreach (var item in AllEventsOpportunitiy)
            {
                var EventOpportunityDetails = new EventOpportunityDetails_Send_ViewModel();
                var ContactDetails = await _unitOfWork.ContactDetailsRepo.GetAllContact_DetailByOpportunitiyID(item.Id);
                var EmailsHistory = await _unitOfWork.SendEmailRepo.GetEmailHistoryByOpportunitiyID(item.Id);
                var EventRequest = _unitOfWork.EventRequesterRepo.GetByID(item.EventRequesterId.GetValueOrDefault());
                var initiat = await _unitOfWork.InitiatedRepo.GetByIdAsync(item.InitiatedId);
                EventOpportunityDetails = new EventOpportunityDetails_Send_ViewModel
                {
                    Opportunity_ID = item.Id,
                    SubmissionDate = item.SubmissionDate.GetValueOrDefault(),
                    OpportunityOwner = _authenticatedUser.UserName,
                    Initiated = new Initiated
                    {
                        Id = initiat.Id,
                        Name = initiat.Name,
                    },
                    EventRequester_ID = item.EventRequesterId.GetValueOrDefault(),
                    EventRequester_Name = EventRequest.Name,
                    CompanyName = item.CompanyCommericalName,
                    ContactDetails = _mapper.Map<List<EventOpportunityContactDetails_ViewModel>>(ContactDetails),
                    SendEmails = new List<SendEmail>(EmailsHistory)
                };
                EventOpportunityDetailsWithoutPagination.Add(EventOpportunityDetails);
            }

            if (EventOpportunityDetailsWithoutPagination.Count > 0) // intiated must return without isDeleted..
            {
                return new Response<IList<EventOpportunityDetails_Send_ViewModel>>(EventOpportunityDetailsWithoutPagination);
            }

            return new Response<IList<EventOpportunityDetails_Send_ViewModel>>(null);
        }




        public async Task<Response<bool>> SaveEventOpportunityDetails(cmd_Post_EventOpportunityStageReport_Parameter request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (string.IsNullOrWhiteSpace(request.OpportunityUpdate))
            {
                return new Response<bool>("Opportunity Update is required.");
            }

            if (await _unitOfWork.OpportunityStageRepo.GetByIdAsync(request.OpportunityStage_ID) == null)
            {
                return new Response<bool>("Opportunity stage Id not found.");
            }

            if (await _unitOfWork.EventSpaceBookingRepo.GetByIdAsync(request.Opportunity_ID) == null)
            {
                return new Response<bool>("Opportunity Id not found.");
            }


            OpportunityStageReport report = new OpportunityStageReport
            {
                Id = 0,
                Date = _dateTimeService.NowUtc,
                OpportunityStageId = request.OpportunityStage_ID,
                Comment = request.OpportunityUpdate,
                Reminder = request.Reminder,
                EventSpaceBookingId = request.Opportunity_ID,
                CreatedBy = _authenticatedUser.UserId
            };

            try
            {
                var opportunityStageReport = await _unitOfWork.OpportunityStageReportRepo.AddAsync(report);
                await _unitOfWork.EventSpaceBookingRepo.UpdateEventOpportunitiyStageReportId(request.Opportunity_ID, request.OpportunityStage_ID);
            }
            catch (Exception ex)
            {
                var response = new Response<bool>(false);
                response.Succeeded = false;
                return response;
            }

            return new Response<bool>(true);
        }

        public async Task<Response<bool>> SendEmails(cmd_Post_SendEmail_Parameter request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            try
            {
                if (string.IsNullOrWhiteSpace(request.Body))
                {
                    return new Response<bool>("Email body should contains text.");
                }
                else if (request.Body.Contains("&#160; &#160; &#160;") || request.Body.Contains("&#160; &#160;&#160;"))
                {
                    return new Response<bool>("Body Should Contain Alphabets.");
                }
                bool CheckExistence = await _unitOfWork.EventSpaceBookingRepo.CheckEventOpportunitiyExistenceByID(request.EventsOpportunities_ID);
                if (!CheckExistence)
                {
                    return new Response<bool>("Event Opportunity Doesn't Exist");
                }

                var emailTemplate = await _unitOfWork.EmailTemplateRepository.GetLatestEmailTemplate(1);
                request.Body = request.Body.Replace("\"", "'");
                var headerImage = "";
                if (emailTemplate != null && emailTemplate.ImagePath != null)
                {
                    headerImage = "https://api.copolitan.com/" + emailTemplate.ImagePath;
                }
                string EncID = await _passwordEncoderDecoder.EncodePasswordToBase64(request.EventsOpportunities_ID.ToString());
                //string DecID = await _encoder.DecodePasswordFromBase64(EncID);

                StringBuilder emailStringBuilder = new StringBuilder();

                emailStringBuilder.Append($"<html lang='en'>");
                emailStringBuilder.Append($"<head></head>");
                emailStringBuilder.Append($"<body>");
                emailStringBuilder.Append($"<div class='pageContainer' style='margin: 0 auto; width: 80%;'>");
                if (emailTemplate != null && emailTemplate.ImagePath != null)
                {
                    emailStringBuilder.Append($"<div class='headerImage' style='background-color: #ba43ff33; display: flex;width: 100%;justify-content: center;height: 240px;margin-top: 20px;'>");
                    emailStringBuilder.AppendFormat("<img src='{0}' alt='cover' style='margin:0 auto;' />", headerImage);
                    emailStringBuilder.Append($"</div>");
                }
                emailStringBuilder.Append(request.Body);
                //emailStringBuilder.Append($"<a href= 'http://eventoppunityredirect.techno-politan.xyz/?id=' + {request.EventsOpportunities_ID}>Complete Submission</a>");
                emailStringBuilder.Append($"<a href= 'https://copolitantest1.techno-politan.xyz/#/membership-booking/eventspace/{EncID}'>Complete Submission</a>");
                emailStringBuilder.Append($"</div>");
                emailStringBuilder.Append($"</body>");
                emailStringBuilder.Append($"</html>");

                var SentEmail = new SendEmail();
                foreach (var item in request.ToUsers)
                {
                    var contact = await _unitOfWork.ContactDetailsRepo.GetContact_DetailByEmail(item);

                    var emailRequest = new EmailRequest();
                    if (request.IsUser != 1)
                    {
                        //var user = await _accountService.GetUserById(_authenticatedUser.UserId);
                        //var DecodedPassword = await _passwordEncoderDecoder.DecodePasswordFromBase64(user.Data.PassEmail);
                        emailRequest = new EmailRequest()
                        {
                            From = request.userName,
                            UserName = _authenticatedUser.UserName,
                            Password = request.password,
                            Body = emailStringBuilder.ToString(),
                            To = contact.Email,
                            CC = request.CC,
                            Subject = request.Subject,
                        };
                    }
                    else if (request.IsUser == 1)
                    {
                        emailRequest = new EmailRequest()
                        {
                            Body = emailStringBuilder.ToString(),
                            To = contact.Email,
                            CC = request.CC,
                            Subject = request.Subject,
                        };
                    }

                    await _emailService.SendAsync(emailRequest, true);

                    SentEmail = new SendEmail
                    {
                        CC = request.CC,
                        Subject = request.Subject,
                        Body = emailStringBuilder.ToString(),
                        ContactDetailId = contact.Id,
                        BookATourId = null,
                        //EventsOpportunitiesId = request.EventsOpportunities_ID,
                        EmailTemplateId = emailTemplate.Id,
                        CreatedAt = _dateTimeService.NowUtc,
                    };
                    SentEmail.FromUser = request.IsUser == 1 ? _mailSettings.ContactUsCopolitan : request.userName;

                    await _unitOfWork.SendEmailRepo.AddAsync(SentEmail);
                }
                return new Response<bool>(true, "Emails Sent Successfully");
            }
            catch (Exception ex) { return new Response<bool>(ex.Message); }
        }

        public async Task<Response<bool>> UpdateOpportunity(cmd_Update_EventOpportunity_Parameter request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var eventOpportunitiy = await _unitOfWork.EventSpaceBookingRepo.GetByIdAsync(request.Opportunity_ID);
            if (eventOpportunitiy == null)
            {
                return new Response<bool>(false, "Opportunity Doesn't Exist.");
            }

            foreach (var item in request.ContactDetails)
            {
                var checkExistanceEmailOrMobileExistInEventspaceBooking = await _unitOfWork.EventSpaceBookingRepo.CheckEmailAndMobileExist(item.Email, item.MobileNumber);
                var checkExistanceEmailOrMobileExistInContactDetails = await _unitOfWork.ContactDetailsRepo.CheckEmailAndMobileExist(item.Email, item.MobileNumber);
                if (checkExistanceEmailOrMobileExistInEventspaceBooking != null)
                {
                    return new Response<bool>($"This user has a previous opportunities with id {checkExistanceEmailOrMobileExistInEventspaceBooking.Id}");
                }
                if (checkExistanceEmailOrMobileExistInContactDetails != null)
                {
                    return new Response<bool>($"This user has a previous opportunities with id {checkExistanceEmailOrMobileExistInContactDetails.Id}");

                }
            }

            eventOpportunitiy.Id = request.Opportunity_ID;
            eventOpportunitiy.SubmissionDate = DateTime.UtcNow;
            eventOpportunitiy.EventRequesterId = request.EventRequester_ID;
            eventOpportunitiy.CompanyCommericalName = request.CompanyName;
            

            await _unitOfWork.EventSpaceBookingRepo.UpdateAsync(eventOpportunitiy);

            await _unitOfWork.ContactDetailsRepo.DeleteContact_DetailByID(request.Opportunity_ID);

            foreach (var item in request.ContactDetails)
            {
                var contact = new ContactDetails
                {
                    Name = item.Name,
                    Email = item.Email,
                    MobileNumber = item.MobileNumber,
                    EventSpaceBookingId = request.Opportunity_ID
                };
                await _unitOfWork.ContactDetailsRepo.AddAsync(contact);
            }
            return new Response<bool>(true, "Event Opportunitiy Updated Successfully.");
        }
    }
}
