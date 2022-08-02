using Dapper;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations
{
    public class WorkSpaceReservationsRepositoryCRM : IWorkSpaceReservationsRepositoryCRM
    {
        private readonly ApplicationDbContext _context;
        public WorkSpaceReservationsRepositoryCRM(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", request.Id);
            parms.Add("@Loc_Id", request.LocationId);
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

            var data = await _context.Connection.QueryAsync<GetAllWorkSpaceReservationsResponse>("dbo.SP_Work_n_Munch_Workspace_Submissions_GetAll_CRM", parms, null, (int) System.Data.CommandType.StoredProcedure);
            return data.AsList();
        }

        public async Task<IReadOnlyList<GatFilteredWorkSpaceReservationResponse>> GetFilteredSubmissions(GatFilteredWorkSpaceReservationDto request)
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

            var data = await _context.Connection.QueryAsync<GatFilteredWorkSpaceReservationResponse>("dbo.SP_Work_n_Munch_Workspace_Submissions_GetAll_CRM", parms, null, (int)System.Data.CommandType.StoredProcedure);

            return data.AsList();
        }
    }
}
