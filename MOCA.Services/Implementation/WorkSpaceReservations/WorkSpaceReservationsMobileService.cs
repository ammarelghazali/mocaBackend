using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Response;
using MOCA.Core.Interfaces.WorkSpaceReservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationsMobileService : IWorkSpaceReservationsMobileService
    {
        public Task<Response<WorkspaceReservationHomePageResponse>> GetWorkSpaceReservationHomePage(WorkspaceReservationHomePageDto request)
        {
            throw new NotImplementedException();

            // TODO: Check if the client id is exists in Basic User
            // Maybe include ReservationsTransactoins and WorkSpaceReservations in this call

            // TODO: Get the latest submitted reservation for the user whether it's
            // Hourly, Tailored, Bundle. we will know that from Reservation type id 

            // TODO: If Hourly, fill the reservatoin, date time, and Hours on WorkspaceReservationHomePageResponse

            // TODO: If Tailored, fill the reservatoin, TailoredStartDate, TailoredEndDate 
            // TailoredHours on WorkspaceReservationHomePageResponse

            // TODO: If Bundle, fill the reservatoin, PackageDeactivationDate, PackageHours 
            // PackageDuration on WorkspaceReservationHomePageResponse
        }
    }
}
