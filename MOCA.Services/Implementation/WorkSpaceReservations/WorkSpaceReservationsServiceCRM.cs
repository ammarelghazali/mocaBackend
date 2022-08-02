using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.WorkSpaceReservations.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationsServiceCRM : IWorkSpaceReservationsServiceCRM
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkSpaceReservationsServiceCRM(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceSubmissionsResponse>>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceSubmissionsDto request)
        {
            var data = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetAllWorkSpaceSubmissions(request);

            if (data.Count > 0)
            {
                return new PagedResponse<IReadOnlyList<GetAllWorkSpaceSubmissionsResponse>>(data, request.pageNumber, request.pageSize, data.Count);
            }
            return new PagedResponse<IReadOnlyList<GetAllWorkSpaceSubmissionsResponse>>(null, request.pageNumber, request.pageSize);
        }
    }
}
