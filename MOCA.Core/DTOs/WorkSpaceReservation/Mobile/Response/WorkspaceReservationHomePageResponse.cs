namespace MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Response
{
    public class WorkspaceReservationHomePageResponse
    {
        public string Reservation { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Hours { get; set; }
        public int? TailoredStartDate { get; set; }
        public int? TailoredEndDate { get; set; }
        public int? TailoredHours { get; set; }
        public DateTime? PackageDeactivationDate { get; set; }
        public int? PackageHours { get; set; }
        public string PackageDuration { get; set; }
    }
}
