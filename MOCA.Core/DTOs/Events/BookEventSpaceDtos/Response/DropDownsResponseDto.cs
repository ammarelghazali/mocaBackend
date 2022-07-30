using MOCA.Core.DTOs.Shared;

namespace MOCA.Core.DTOs.Events.BookEventSpaceDtos.Response
{
    public class DropDownsResponseDto
    {
        public List<DropdownViewModel> Locations { get; set; }
        public List<DropdownViewModel> EventRequesters { get; set; }
        public List<DropdownViewModel> EventIndusties { get; set; }
        public List<DropdownViewModel> EventCategories { get; set; }
        public List<DropdownViewModel> EventReccurances { get; set; }
        public List<DropdownViewModel> EventTypes { get; set; }
        public List<DropdownViewModel> EventVenues { get; set; }
        public List<DropdownViewModel> EventAttendances { get; set; }
        public List<DropdownViewModel> Initiated { get; set; }
    }

}
