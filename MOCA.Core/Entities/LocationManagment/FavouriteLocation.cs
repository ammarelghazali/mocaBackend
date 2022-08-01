using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.SSO;

namespace MOCA.Core.Entities.LocationManagment
{
    public class FavouriteLocation : BaseEntity
    {
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public long BasicUserId { get; set; }
        public virtual BasicUser BasicUser { get; set; }
    }
}
