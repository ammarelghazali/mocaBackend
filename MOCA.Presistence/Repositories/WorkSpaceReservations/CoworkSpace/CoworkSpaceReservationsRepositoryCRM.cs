using Dapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Repositories;
using MOCA.Presistence.Contexts;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations.CoworkSpace
{
    public class CoworkSpaceReservationsRepositoryCRM : ICoworkSpaceReservationsRepositoryCRM
    {
        private readonly ApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public CoworkSpaceReservationsRepositoryCRM(ApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }
        public async Task<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>> GetFilteredSubmissions(GetFilteredWorkSpaceReservationDto request)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", request.Id);
            parms.Add("@Loc_Id", request.LocationId);
            //parms.Add("@Platform", request.Platform);
            parms.Add("@From_Date_Time", request.FromDateTime == null ? null : request.FromDateTime.Value.ToShortDateString());
            parms.Add("@To_Date_Time", request.ToDateTime == null ? null : request.ToDateTime.Value.ToShortDateString());
            parms.Add("@Name", request.Name);
            parms.Add("@Country_Code", request.CountryCode);
            parms.Add("@Mobile_Number", request.MobileNumber);
            parms.Add("@Mode", request.Mode);
            parms.Add("@Credit_Hours", request.CreditHours);
            parms.Add("@MaxCredit_Hours", request.MaxCreditHours);
            parms.Add("@Amount", request.Amount);
            parms.Add("@MaxAmount", request.MaxAmount);
            parms.Add("@Payment_Method", request.PaymentMethodId);
            parms.Add("@From_End_Date", request.FromEndDate == null ? null : request.FromEndDate.Value.ToShortDateString());
            parms.Add("@To_End_Date", request.ToEndDate == null ? null : request.ToEndDate.Value.ToShortDateString());
            parms.Add("@Entry_Scan_Time", request.EntryScanTime);
            parms.Add("@Opportunity_Start_Date", request.OpportunityStartDate);
            parms.Add("@Status", request.Status);
            parms.Add("@PlanDay_Type", request.ReservationType);
            parms.Add("@Client_Id", request.ClientId);
            parms.Add("@SearchValue", request.SearchValue);
            parms.Add("@pageNumber", request.pageNumber);
            parms.Add("@pageSize", request.pageSize);
            parms.Add("@SortBy", request.SortBy);
            parms.Add("@SortDirection", request.SortDirection);

            var data = await _context.Connection.QueryAsync<GetFilteredWorkSpaceReservationResponse>("dbo.SP_Work_n_Munch_Workspace_Submissions_GetAll_CRM", parms, null, (int)System.Data.CommandType.StoredProcedure);

            return data.AsList();
        }

        public async Task<IReadOnlyList<GetFilteredWorkSpaceReservationNotPaginatedResponse>> GetFilteredSubmissionsWithoutPagination(GetAllWorkSpaceReservationNotPaginated request)
        {
            DynamicParameters parms = new DynamicParameters();
            //parms.Add("@Platform", request.Platform);
            parms.Add("@Date_Time", request.Date);
            parms.Add("@First_Name", request.FirstName);
            parms.Add("@Last_Name", request.LastName);
            //parms.Add("@Country_Code", request.Country_Code);
            parms.Add("@Mobile_Number", request.MobileNumber);
            parms.Add("@Mode", request.Mode);
            parms.Add("@Credit_Hours", request.CreditHours);
            parms.Add("@Amount", request.Amount);
            parms.Add("@Payment_Method", request.PaymentMethodId);
            parms.Add("@End_Date", request.EndDate);
            parms.Add("@Entry_Scan_Time", request.EntryScanTime);
            parms.Add("@Opportunity_Start_Date", request.OpportunityStartDate);
            parms.Add("@Status", request.Status);
            parms.Add("@PlanDay_Type", request.PlanDayType);
            parms.Add("@Client_Id", request.ClientId);

            var data = await _context.Connection.QueryAsync<GetFilteredWorkSpaceReservationNotPaginatedResponse>("[dbo].[SP_Work_n_Munch_Workspace_Submissions_GetAll_CRM_WithoutPagination]", parms, null, (int)System.Data.CommandType.StoredProcedure);

            return data.AsList();
        }

        public async Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns()
        {
            return await _context.ReservationTransactions.Where(r => r.ReservationTypeId == 3 && r.ReservationTypeId == 4 &
                                                                                    r.ReservationTypeId == 5 && r.IsDeleted != true &&
                                                                                    r.ExtendExpiryDate > _dateTimeService.NowUtc)
                                                                        .Include(r => r.Location)
                                                                        .Select(r => new DropdownViewModel
                                                                        {
                                                                            Id = r.LocationId,
                                                                            Name = r.Location.Name
                                                                        }).Distinct().ToListAsync();
        }
    }
}
