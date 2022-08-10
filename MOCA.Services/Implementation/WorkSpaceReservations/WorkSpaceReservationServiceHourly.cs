using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using MOCA.Core.Enums.Shared;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationServiceHourly : IWorkSpaceReservationServiceHourly
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IReservationsStatusService _reservationsStatusService;

        public WorkSpaceReservationServiceHourly(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService, 
                                                 IReservationsStatusService reservationsStatusService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _reservationsStatusService = reservationsStatusService;
        }

        public async Task<Response<SharedCreationResponse>> CreateTopUp(CreateWorkSpaceTopUp topUp)
        {
            throw new NotImplementedException();

            var reservation = await _unitOfWork.WorkSpaceReservationHourlyRepo.GetReservationById(topUp.WorkspaceReservationId);


            // 1. get availability of working hours by computing (endWorking hour - (EndTimeDate + TopUpHour))


            // 2. get count of active tailoreds
               // get count of active Bundle
               // get count of active hourly

            // 3. max occupancy > TotalCount


            // check if it exceeds the available hours and Available occupancy

            // get the maximum occupancy of the work space

            // get the time of the ending of the current reservation by
            // 1. reservation.date + hours (of hourId)
            // 2. ExpiryEndDate from reservationTransaction

            // get the number of reservations of the workspace in the time of the top up houlry, tailored, bundle
            //  the time of the ending of the current reservation +  TopUpHours (of hourId)

            //  maximum occupancy - number of reservations if <= 0 then refuse it

            int reservationDay = (int)Enum.Parse(typeof(WeekDays), Convert.ToDateTime(reservation.Date).DayOfWeek.ToString());

            List<int> listOfHours = new List<int> { 1, 2, 4, 6, 8 };

            foreach (var item in reservation.Location.LocationWorkingHours)
            {
                int dayFrom = (int)Enum.Parse(typeof(WeekDays), item.DayFrom);
                int dayTo = (int)Enum.Parse(typeof(WeekDays), item.DayTo);

                for (int day = dayFrom; day <= dayTo; day++)
                {
                    if (reservationDay == day)
                    {
                        //get remaining Hours and the maximum hours that can be Topped Up

                        TimeSpan remainingHours = Convert.ToDateTime(item.EndWorkingHour) - DateTime.Now;

                        int remainingHoursBeforeClosing = Convert.ToInt32(remainingHours.Hours);

                        int closest = listOfHours.Aggregate((x, y) => Math.Abs(x - remainingHoursBeforeClosing) < Math.Abs(y - remainingHoursBeforeClosing) ? x : y);
                        
                        if (topUp.NumberOfHours > closest)
                            return new Response<SharedCreationResponse>("Maximum hours are " + closest);

                        break;
                    }
                }

            }

            // get the calculated price


            // add the top up
            var reservationTopUp = new WorkSpaceHourlyTopUp
            {
                Description = topUp.Description,
                HourId = topUp.HourId ?? 0,
                // HourlyTotalPrice = calculated price
                WorkSpaceReservationHourlyId = topUp.WorkspaceReservationId,
            };

            _unitOfWork.WorkSpaceHourlyTopUpRepo.Insert(reservationTopUp);

            // update  remaining hours with calculating the price using LoungqLocationPrice

        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request)
        {
            var hourlyReservations = await _unitOfWork.WorkSpaceReservationHourlyRepo.GetAllWorkSpaceSubmissions(request);

            return await hourlyReservations.Skip(request.pageSize * (request.pageNumber - 1)).Take(request.pageSize).ToListAsync();
        }

        public async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.WorkSpaceReservationHourlyRepo
                                             .GetReservationInfo(request.WorkSpaceReservationId);

            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }

            // Get Entry Scan Time 

            var entryScanTime = reservation.WorkSpaceHourlyTransactions.ReservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;

        
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
                        Amount = item.HourlyTotalPrice,
                        CreatedAt = item.CreatedAt,
                        CreatedBy = item.CreatedBy,
                        Type = "Hourly",
                        //Hours = // from LoungeLocationPricing
                        Payment_Method = item.PaymentMethod.Name
                    });
                }
                else
                {
                    giftedHours.Add(new GiftedHours
                    {
                        Reason = item.Description,
                        AdminName = item.CreatedBy,
                        CreatedAt = item.CreatedAt,
                        //HoursAdded = // from LoungeLocationPricing, 
                    });
                }
            }

            var reservationInfo = new WorkSpaceReservationHistoryResponse
            {
                Id = reservation.Id,
                BasicUserId = reservation.BasicUserId,
                FirstName = reservation.BasicUser.FirstName,
                LastName = reservation.BasicUser.LastName,
                Amount = reservation.Price,
                PaymentMethod = reservation.PaymentMethodId,
                Status = _reservationsStatusService.GetStatus(reservation.WorkSpaceHourlyTransactions.ReservationTransaction, 
                                                              reservation.WorkSpaceHourlyCancellation.CancelReservation),
                Mode = reservation.TopUps.Count == 0 ? "Basic" : "TopUp",
                ReservationType = "Hourly",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                GiftedHours = giftedHours,
                TopupHistory = topupHistory,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = reservation.WorkSpaceHourlyTransactions.ReservationTransaction.ExtendExpiryDate,
                CreditHours = reservation.WorkSpaceHourlyTransactions.ReservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.Date,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                ReservationDetails = reservation.WorkSpaceHourlyTransactions.ReservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);
        }
    }
}
