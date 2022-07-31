namespace MOCA.Core.DTOs.Events.EventAttendanceDtos.Request
{
    public class get_AllEventAttendance_Parameter
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
