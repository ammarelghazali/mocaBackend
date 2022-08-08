namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetAllWorkSpaceReservationsDto
    {
        public GetAllWorkSpaceReservationsDto(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            this.pageSize = pageSize <= 0 ? 10 : pageSize;
        }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
