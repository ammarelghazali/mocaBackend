﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationsServiceCRM : IWorkSpaceReservationsServiceCRM
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWorkSpaceReservationServiceHourly _hourlyService;
        private readonly IWorkSpaceReservationServiceBundle _bundleService;
        private readonly IWorkSpaceReservationServiceTailored _tailoredService;

        public WorkSpaceReservationsServiceCRM(IUnitOfWork unitOfWork, IMapper mapper, 
                                               IWorkSpaceReservationServiceHourly hourlyService,
                                               IWorkSpaceReservationServiceTailored tailoredService,
                                               IWorkSpaceReservationServiceBundle bundleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hourlyService = hourlyService;
            _bundleService = bundleService;
            _tailoredService = tailoredService;
        }

        public IWorkSpaceReservationServiceTailored TailoredService { get; }

        public async Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var hourlyReservations = await _unitOfWork.WorkSpaceReservationHourlyRepo.GetAllWorkSpaceSubmissions(request);
            var tailoredReservations = await _unitOfWork.WorkSpaceReservationTailoredRepo.GetAllWorkSpaceSubmissions(request);
            var bundleReservations = await _unitOfWork.WorkSpaceReservationBundleRepo.GetAllWorkSpaceSubmissions(request);

            var allReservations =  hourlyReservations.Union(tailoredReservations)
                                                     .Union(bundleReservations)
                                                     .OrderByDescending(r => r.OpportunityStartDate);
                                                        
            var paginatedReservationTask =  allReservations.Skip(request.pageSize * (request.pageNumber - 1))
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
                                                                                                                       request.pageSize) );
            }
            return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(null, request.pageNumber, request.pageSize);
        }

        public async Task<PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>> GetFilteredSubmissions(GetFilteredWorkSpaceReservationDto request)
        {
            var data = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetFilteredSubmissions(request);

            if (data.Count > 0)
            {
                return new PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>(data, request.pageNumber, request.pageSize, 
                                                                                                 data.Count);

            }
            return new PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>(null, request.pageNumber, request.pageSize);
        }

        public async Task<Response<IReadOnlyList<GetFilteredWorkSpaceReservationNotPaginatedResponse>>> GetFilteredSubmissionsWithoutPagination(GetAllWorkSpaceReservationNotPaginated request)
        {
            var data = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetFilteredSubmissionsWithoutPagination(request);

            if (data.Count > 0)
            {
                return new Response<IReadOnlyList<GetFilteredWorkSpaceReservationNotPaginatedResponse>>(data);
            }

            return new Response<IReadOnlyList<GetFilteredWorkSpaceReservationNotPaginatedResponse>>(null, "There's No Related Data");
        }

        public async Task<Response<WorkSpaceReservationLocationsDropDown>> GetWorkSpaceLocationsDropDowns()
        {
            var locations = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetWorkSpaceLocationsDropDowns();

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
            else if(request.ReservationTypeId == 2)
            {
                return await _tailoredService.GetReservationInfo(request);
            }
            else if(request.ReservationTypeId == 3)
            {
                return await _bundleService.GetReservationInfo(request);
            }

            return new Response<WorkSpaceReservationHistoryResponse>("ReservationTypeId is not correct");
        }

    }
}
