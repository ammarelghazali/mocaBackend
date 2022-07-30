using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class EventOpportunityContactDetail_ViewModel
    {
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Name should contain characters only.")]
        [MaxLength(450)]
        public string? Name { get; set; }
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please enter correct email format.")]
        [MaxLength(450)]
        public string? Email { get; set; }
        [RegularExpression(@"^(\+[0-9]+)$", ErrorMessage = "Please enter correct mobile number format.")]
        [MaxLength(450)]
        public string? MobileNumber { get; set; }
    }
}
