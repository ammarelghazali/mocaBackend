using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Request;
using MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.Events
{
    public class EventsOpportunitiesService : IEventsOpportunitiesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IMapper _mapper;
        public EventsOpportunitiesService(IUnitOfWork unitOfWork,
                                          IAuthenticatedUserService authenticatedUser,
                                          IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authenticatedUser = authenticatedUser ?? throw new ArgumentNullException(nameof(authenticatedUser));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<Response<GetEmailTempleteEventOpportunitylViewModelDto>> GetEmailTempletType(GetEmailTempleteEventOpportunityDto request)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var eventOpportunity = await _unitOfWork.EmailTemplateRepository.GetLatestEmailTemplate(request.EmailTemplateTypeID);
            if (eventOpportunity == null)
            {
                return new Response<GetEmailTempleteEventOpportunitylViewModelDto>(null, "There is no email templates.");
            }

            var eventOpportunityResult = _mapper.Map<GetEmailTempleteEventOpportunitylViewModelDto>(eventOpportunity);

            if (eventOpportunity != null)
            {
                return new Response<GetEmailTempleteEventOpportunitylViewModelDto>(eventOpportunityResult);
            }

            return new Response<GetEmailTempleteEventOpportunitylViewModelDto>(null, "No Templates Created Before");
        }



    }

}
