namespace MOCA.Core.DTOs.Events.EventAttendanceDtos.Request
{
    public class GetAllEventAttendanceParameter
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
