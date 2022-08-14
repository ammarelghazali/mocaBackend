using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations.CoworkSpace
{
	public class CoworkSpaceReservationServiceCRM : ICoworkSpaceReservationServiceCRM
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ICoworkSpaceReservationServiceHourly _hourlyService;
		private readonly ICoworkSpaceReservationServiceBundle _bundleService;
		private readonly ICoworkSpaceReservationServiceTailored _tailoredService;

		public CoworkSpaceReservationServiceCRM(IUnitOfWork unitOfWork, IMapper mapper,
											   ICoworkSpaceReservationServiceHourly hourlyService,
											   ICoworkSpaceReservationServiceTailored tailoredService,
											   ICoworkSpaceReservationServiceBundle bundleService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_hourlyService = hourlyService;
			_bundleService = bundleService;
			_tailoredService = tailoredService;
		}

		public async Task<Response<SharedCreationResponse>> AddGiftedHours(CreateWorkSpaceTopUp topUp)
		{
			if (topUp.ReservationTypeId == 1)
			{
				return await _hourlyService.CreateTopUp(topUp);
			}
			if (topUp.ReservationTypeId == 2)
			{
				return await _tailoredService.CreateTopUp(topUp);
			}

			return new Response<SharedCreationResponse>("Reservation Type Id is wrong");
		}

		public async Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
		{
			var hourlyReservations = _unitOfWork.CoworkSpaceReservationHourlyRepo.GetAllWorkSpaceSubmissions(request);
			var tailoredReservations = _unitOfWork.CoworkSpaceReservationTailoredRepo.GetAllWorkSpaceSubmissions(request);
			var bundleReservations = _unitOfWork.CoworkSpaceReservationBundleRepo.GetAllWorkSpaceSubmissions(request);

			var allReservations = hourlyReservations.Union(tailoredReservations)
													 .Union(bundleReservations)
													 .OrderByDescending(r => r.OpportunityStartDate);

			var paginatedReservationTask = allReservations.Skip(request.pageSize * (request.pageNumber - 1))
													  .Take(request.pageSize).ToListAsync();

			var countReservationTask = allReservations.CountAsync();

			var tasks = Task.WhenAll(paginatedReservationTask, countReservationTask);

			try
			{
				await tasks;
			}
			catch (Exception e)
			{

				throw tasks.Exception ?? throw new Exception("Error Happend in Getting All Reservaitons");
			}

			var paginatedReservations = paginatedReservationTask.Result;
			var countReservations = countReservationTask.Result;

			if (paginatedReservations.Count > 0)
			{
				return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(paginatedReservations,
																							 request.pageNumber,
																							 request.pageSize,
																							 (int)Math.Ceiling((double)countReservations /
																													   request.pageSize));
			}
			return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(null, request.pageNumber, request.pageSize);
		}

		public async Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>> GetAllWorkSpaceSubmissionsSP(RequestParameter request)
		{
			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("@pageNumber", request.PageNumber);
			parameters.Add("@pageSize", request.PageSize);

			var data = await _unitOfWork.CoworkSpaceReservationHourlyRepo.QueryAsync<GetAllWorkSpaceReservationsResponse>("[dbo].[SP_GetAllCoworkSpaceSubmissionsCRM]", parameters, System.Data.CommandType.StoredProcedure);

			if (data.Count > 0)
			{
				var totalCount = await _unitOfWork.CoworkSpaceReservationHourlyRepo.QueryAsync<int>("[dbo].[SP_GetCoworkSpaceSubmissionsTotalCount]", null, System.Data.CommandType.StoredProcedure);


				return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(data, request.PageNumber, request.PageSize,
																								  (int)Math.Ceiling((double)totalCount[0]/
																													   request.PageSize));
			}

			return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(null, request.PageNumber, request.PageSize);
		}

		public async Task<PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>> GetFilteredSubmissions(GetFilteredWorkSpaceReservationDto request)
		{
			DynamicParameters parms = new DynamicParameters();
			parms.Add("@Id", request.Id);
			parms.Add("@LocationId", request.LocationId);
			parms.Add("@FromDateTime", request.FromDateTime == null ? null : request.FromDateTime.Value.ToShortDateString());
			parms.Add("@ToDateTime", request.ToDateTime == null ? null : request.ToDateTime.Value.ToShortDateString());
			parms.Add("@Name", request.Name);
			parms.Add("@MobileNumber", request.MobileNumber);
			parms.Add("@Mode", request.Mode);
			parms.Add("@CreditHours", request.CreditHours);
			parms.Add("@MaxCreditHours", request.MaxCreditHours);
			parms.Add("@Amount", request.Amount);
			parms.Add("@MaxAmount", request.MaxAmount);
			parms.Add("@PaymentMethod", request.PaymentMethodId);
			parms.Add("@FromEndDate", request.FromEndDate == null ? null : request.FromEndDate.Value.ToShortDateString());
			parms.Add("@ToEndDate", request.ToEndDate == null ? null : request.ToEndDate.Value.ToShortDateString());
			parms.Add("@EntryScanTime", request.EntryScanTime);
			parms.Add("@OpportunityStartDate", request.OpportunityStartDate);
			parms.Add("@Status", request.Status);
			parms.Add("@ReservationType", request.ReservationType);
			parms.Add("@ClientId", request.ClientId);
			parms.Add("@SearchValue", request.SearchValue);
			parms.Add("@pageNumber", request.pageNumber);
			parms.Add("@pageSize", request.pageSize);
			parms.Add("@SortBy", request.SortBy);
			parms.Add("@SortDirection", request.SortDirection);

			var data = await _unitOfWork.CoworkSpaceReservationHourlyRepo.QueryAsync<GetFilteredWorkSpaceReservationResponse>("[dbo].[GetAllFilteredCoworkingSpaceSubmissions]", parms, System.Data.CommandType.StoredProcedure);

			if (data.Count > 0)
			{
				var totalCount = await _unitOfWork.CoworkSpaceReservationHourlyRepo.QueryAsync<int>("[dbo].[SP_GetFilteredCoworkSpaceSubmissionsTotalCount]", parms, System.Data.CommandType.StoredProcedure);

				return new PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>(data, request.pageNumber, request.pageSize,
																								 (int)Math.Ceiling((double)totalCount[0] /
																													   request.pageSize));

			}
			return new PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>(null, request.pageNumber, request.pageSize);
		}

		public async Task<Response<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>> GetFilteredSubmissionsWithoutPagination(GetAllWorkSpaceReservationNotPaginated request)
		{
			DynamicParameters parms = new DynamicParameters();
			parms.Add("@Id", request.Id);
			parms.Add("@LocationId", request.LocationId);
			parms.Add("@FromDateTime", request.FromDateTime == null ? null : request.FromDateTime.Value.ToShortDateString());
			parms.Add("@ToDateTime", request.ToDateTime == null ? null : request.ToDateTime.Value.ToShortDateString());
			parms.Add("@Name", request.Name);
			parms.Add("@MobileNumber", request.MobileNumber);
			parms.Add("@Mode", request.Mode);
			parms.Add("@CreditHours", request.CreditHours);
			parms.Add("@MaxCreditHours", request.MaxCreditHours);
			parms.Add("@Amount", request.Amount);
			parms.Add("@MaxAmount", request.MaxAmount);
			parms.Add("@PaymentMethod", request.PaymentMethodId);
			parms.Add("@FromEndDate", request.FromEndDate == null ? null : request.FromEndDate.Value.ToShortDateString());
			parms.Add("@ToEndDate", request.ToEndDate == null ? null : request.ToEndDate.Value.ToShortDateString());
			parms.Add("@EntryScanTime", request.EntryScanTime);
			parms.Add("@OpportunityStartDate", request.OpportunityStartDate);
			parms.Add("@Status", request.Status);
			parms.Add("@ReservationType", request.ReservationType);
			parms.Add("@ClientId", request.ClientId);
			parms.Add("@SearchValue", request.SearchValue);
			parms.Add("@SortBy", request.SortBy);
			parms.Add("@SortDirection", request.SortDirection);

			var data = await _unitOfWork.CoworkSpaceReservationHourlyRepo.QueryAsync<GetFilteredWorkSpaceReservationResponse>("[dbo].[SP_GetAllFilteredCoworkingSpaceSubmissionsWithoutPagination]", parms, System.Data.CommandType.StoredProcedure);

			
			return new Response<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>(data);
		}

		public async Task<Response<WorkSpaceReservationLocationsDropDown>> GetWorkSpaceLocationsDropDowns()
		{
			var locations = await _unitOfWork.CoworkSpaceReservationsRepositoryCRM.GetWorkSpaceLocationsDropDowns();

			var workSpaceReservationLocations = new WorkSpaceReservationLocationsDropDown
			{
				Locations = locations
			};

			return new Response<WorkSpaceReservationLocationsDropDown>(workSpaceReservationLocations);
		}

		public async Task<Response<WorkSpaceReservationHistoryResponse>> GetWorkSpaceOpportunityInfoHistory(GetWorkSpaceReservationHistoryDto request)
		{
			if (request.ReservationTypeId == 1)
			{
				return await _hourlyService.GetReservationInfo(request);
			}
			else if (request.ReservationTypeId == 2)
			{
				return await _tailoredService.GetReservationInfo(request);
			}
			else if (request.ReservationTypeId == 3)
			{
				return await _bundleService.GetReservationInfo(request);
			}

			return new Response<WorkSpaceReservationHistoryResponse>("ReservationTypeId is not correct");
		}
	}
}
