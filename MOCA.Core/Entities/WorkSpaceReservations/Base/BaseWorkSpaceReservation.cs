using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.SSO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.Base
{
    public class BaseWorkSpaceReservation : BaseReservationEntity
    {
        [Required]
        public long WorkSpaceId { get; set; }
        //TODO: Add Relation To WorkSpace

        [ForeignKey("WorkSpaceId")]
        public WorkSpace WorkSpace { get; set; }
    }
}
