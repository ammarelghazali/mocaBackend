using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventOpportunityContactDetails_ViewModel
    {
        public long id { get; set; }
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Name should contain characters only.")]
        [MaxLength(450)]
        public string name { get; set; }
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please enter correct email format.")]
        [MaxLength(450)]
        public string email { get; set; }
        [RegularExpression(@"^(\+[0-9]+)$", ErrorMessage = "Please enter correct mobile number format.")]
        [MaxLength(450)]
        public string mobileNumber { get; set; }
    }
}
