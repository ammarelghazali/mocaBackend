using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Entities.MocaSetting;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<EventSpaceBooking> EventSpaceBookings { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Faq> Faqs { get; set; }
        public ICollection<IssueCaseStage> IssueCaseStages { get; set; }
        public ICollection<IssueReport> IssueReports { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<Policy> Plocies { get; set; }
        public ICollection<TopUp> TopUps { get; set; }
        public ICollection<Wifi> Wifi { get; set; }
    }
}
