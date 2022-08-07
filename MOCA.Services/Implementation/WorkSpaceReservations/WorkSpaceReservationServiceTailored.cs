using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationServiceTailored : IWorkSpaceReservationServiceTailored
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;

        public WorkSpaceReservationServiceTailored(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<SharedCreationResponse>> CreateTopUp(CreateWorkSpaceTopUp topUp)
        {
            throw new NotImplementedException();

            var reservation = await _unitOfWork.WorkSpaceReservationTailoredRepo.GetReservationById(topUp.WorkspaceReservationId);

            // Check if it exceeds Availability

            // check if it exceeds the maximum available hours

            if (reservation.TailoredHours + topUp.TailoredHours > 160)
                return new Response<SharedCreationResponse>("The Maximum Hours is 160 hours");

            var remainingDays = (Convert.ToDateTime(reservation.TailoredEndDate) - DateTime.Now).Days;

            //  int countWorkingHours = (Convert.ToDateTime(itm.WH_End) - Convert.ToDateTime(itm.WH_Start)).Hours;
            int maxHours = (remainingDays * 8);

            // add the top up
            var reservationTopUp = new WorkSpaceTailoredTopUp
            {
                Description = topUp.Description,
                TailoredHours = topUp.TailoredHours ?? 0,
                WorkSpaceReservationTailoredId = topUp.WorkspaceReservationId,
                //TailoredPrice = using tailored
            };

            _unitOfWork.WorkSpaceTailoredTopUpRepo.Insert(reservationTopUp);

            // update  remaining hours using tailored

            // get the calculated price
        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request)
        {
            var tailoredReservations = await _unitOfWork.WorkSpaceReservationTailoredRepo.GetAllWorkSpaceSubmissions(request);

            return await tailoredReservations.Skip(request.pageSize * (request.pageNumber - 1)).Take(request.pageSize).ToListAsync();
        }

        public async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.WorkSpaceReservationTailoredRepo
                                                           .GetReservationInfo(request.WorkSpaceReservationId);


            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }


            // Get Entry Scan Time 

            var entryScanTime = reservation.WorkSpaceTailoredTransactions.ReservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;

            // Get Reservation Status
            string status;

            var expiryDate = reservation.WorkSpaceTailoredTransactions.ReservationTransaction.ExtendExpiryDate ?? null;

            if (expiryDate is null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's missing Reservation info");
            }

            var isExpired = DateTime.Compare(_dateTimeService.NowUtc, expiryDate.Value);

            if (isExpired > 0 || isExpired == 0)
                status = "Closed";

            else
            {
                var isScannedIn = reservation.WorkSpaceTailoredTransactions.ReservationTransaction.ReservationDetails.Count > 0;

                status = isScannedIn ? "Open" : "New";
            }

            // List of Foodics Details

            // Set top ups history, and Gifted Hours
            List<WorkSpaceTopupHistoryResponse> topupHistory = new List<WorkSpaceTopupHistoryResponse>();
            List<GiftedHours> giftedHours = new List<GiftedHours>();

            foreach (var item in reservation.TopUps)
            {
                if (item.Description == null)
                {
                    topupHistory.Add(new WorkSpaceTopupHistoryResponse
                    {
                        Id = item.Id,
                        Amount = item.TailoredHours,
                        CreatedAt = item.CreatedAt,
                        CreatedBy = item.CreatedBy,
                        Type = "Tailored",
                        Payment_Method = item.PaymentMethod.Name,
                        Hours = item.TailoredHours,
                    });
                }
                else
                {
                    giftedHours.Add(new GiftedHours
                    {
                        Reason = item.Description,
                        AdminName = item.CreatedBy,
                        CreatedAt = item.CreatedAt,
                        HoursAdded = item.TailoredHours,
                    });
                }
            }

            var reservationInfo = new WorkSpaceReservationHistoryResponse
            {
                Id = reservation.Id,
                BasicUserId = reservation.BasicUserId,
                FirstName = reservation.BasicUser.FirstName,
                LastName = reservation.BasicUser.LastName,
                Amount = reservation.TailoredPrice,
                PaymentMethod = reservation.PaymentMethodId,
                Status = status,
                Mode = reservation.TopUps.Count == 0 ? "Basic" : "TopUp",
                ReservationType = "Tailored",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                GiftedHours = giftedHours,
                TopupHistory = topupHistory,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = expiryDate,
                CreditHours = reservation.WorkSpaceTailoredTransactions.ReservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.TailoredStartDate,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                ReservationDetails = reservation.WorkSpaceTailoredTransactions.ReservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);
        }
    }
}
