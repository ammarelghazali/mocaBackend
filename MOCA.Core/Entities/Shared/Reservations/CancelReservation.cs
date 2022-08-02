using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.SSO;
using MOCA.Core.Entities.SSO.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class CancelReservation : BaseEntity
    {
        [Required]
        public string ReservationType { get; set; }

        // TODO: Column to determinte the plan days for workspace or multiple ids for each type

        [Required]
        public DateTime CancelDate { get; set; }

        [Required]
        public string BasicUserId { get; set; }

        [ForeignKey("BasicUserId")]
        public BasicUser BasicUser{ get; set; }

        public string? AdminId { get; set; }

        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }

        public long? WorkSpaceReservationId { get; set; }
        //TODO: Relation with WorknMunchWorkSpace
        public long? MeetingRoomReservationId { get; set; }
        //TODO: Relation with WorknMunchMeetingRooms

        public long? BizLoungeReservationId { get; set; }
        //TODO: Relation with WorknMunchBizLounge
    }
}
